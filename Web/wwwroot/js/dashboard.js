// ── NotificationManager ────────────────────────────
const NotificationManager = (() => {
    const ICONS = { warning: '!', error: '!', info: 'i' };
    let items = [];

    function render() {
        const list  = document.getElementById('notifList');
        const badge = document.getElementById('notifBadge');

        if (items.length === 0) {
            list.innerHTML = '<li class="notif-empty">Sin notificaciones</li>';
            badge.hidden = true;
            return;
        }

        list.innerHTML = items.map((n, i) => `
            <li class="notif-item ${n.type}" data-index="${i}">
                <span class="notif-item-icon">${ICONS[n.type] ?? '•'}</span>
                <div class="notif-item-body">
                    <div class="notif-item-title">${n.title}</div>
                    <div class="notif-item-msg">${n.message}</div>
                </div>
            </li>
        `).join('');

        badge.textContent = items.length;
        badge.hidden = false;
    }

    function add(type, title, message, source = null) {
        items.push({ type, title, message, source });
        render();
    }

    function clearSource(source) {
        items = items.filter(n => n.source !== source);
        render();
    }

    function clearAll() {
        items = [];
        render();
    }

    // Toggle dropdown
    document.getElementById('notifBell').addEventListener('click', e => {
        e.stopPropagation();
        document.getElementById('notifDropdown').classList.toggle('open');
    });

    document.addEventListener('click', e => {
        if (!document.getElementById('notifWrapper').contains(e.target))
            document.getElementById('notifDropdown').classList.remove('open');
    });

    document.getElementById('notifClearAll').addEventListener('click', clearAll);

    return { add, clearSource, clearAll };
})();

let currentRows = [];
let filteredRows = [];
let selectedStatus = 'activo';
let currentPage = 1;
const pageSize = 10;

const dashboardData = JSON.parse(
    document.getElementById("dashboard-data").textContent
);
const modules = dashboardData.modules;
const currentRole = dashboardData.role;
const currentIdTrabajador = dashboardData.idTrabajador;
const apiBaseUrl = dashboardData.apiBaseUrl;

const state = {
    selected: modules[0]?.key ?? null,
    module: null
};

const crudRoutes = {
    usuarios: '/Usuario',
    planilla: '/Trabajador',
    puestos: '/Puesto',
    proveedores: '/Proveedor',
    productos: '/Producto',
    inventario: '/Inventario',
    produccion: '/Produccion',
    clientes: '/Cliente'
};

const els = {
    sidebar:       document.getElementById('sidebar'),
    title:         document.getElementById('moduleTitle'),
    tag:           document.getElementById('moduleTag'),
    description:   document.getElementById('moduleDescription'),
    tableTitle:    document.getElementById('tableTitle'),
    tableStatus:   document.getElementById('tableStatus'),
    tableWrap:     document.getElementById('tableWrap'),
    createButton:  document.getElementById('createButton')
};

const sidebarUi = {
    shell: document.querySelector('.page-shell'),
    toggle: document.getElementById('sidebarToggle'),
    backdrop: document.getElementById('sidebarBackdrop')
};

const SIDEBAR_HIDDEN_KEY = 'pt.sidebar.hidden';
let sidebarHidden = localStorage.getItem(SIDEBAR_HIDDEN_KEY) === '1';

function persistSidebarState() {
    localStorage.setItem(SIDEBAR_HIDDEN_KEY, sidebarHidden ? '1' : '0');
}

function applySidebarState() {
    sidebarUi.shell?.classList.toggle('sidebar-hidden', sidebarHidden);

    if (sidebarUi.toggle) {
        sidebarUi.toggle.setAttribute('aria-pressed', String(!sidebarHidden));
        sidebarUi.toggle.title = sidebarHidden ? 'Mostrar menu' : 'Ocultar menu';
    }
}

// ── Toast ──────────────────────────────────────────────
let toastTimer;
function showToast(message, type = 'success') {
    const toast = document.getElementById('toast');
    toast.textContent = message;
    toast.className = `toast toast-${type} show`;
    clearTimeout(toastTimer);
    toastTimer = setTimeout(() => toast.classList.remove('show'), 3500);
}

// ── Confirm personalizado ──────────────────────────────
function showConfirm(message, okLabel = 'Confirmar', okStyle = 'danger') {
    return new Promise(resolve => {
        const overlay = document.getElementById('confirmOverlay');
        const okBtn   = document.getElementById('confirmOk');
        document.getElementById('confirmMsg').textContent = message;
        okBtn.textContent = okLabel;
        okBtn.className = `mf-btn mf-btn-${okStyle}`;
        overlay.classList.add('show');

        const ok     = document.getElementById('confirmOk');
        const cancel = document.getElementById('confirmCancel');

        function cleanup(result) {
            overlay.classList.remove('show');
            ok.replaceWith(ok.cloneNode(true));
            cancel.replaceWith(cancel.cloneNode(true));
            resolve(result);
        }

        document.getElementById('confirmOk').addEventListener('click', () => cleanup(true));
        document.getElementById('confirmCancel').addEventListener('click', () => cleanup(false));
    });
}

// ── Modal ──────────────────────────────────────────────
function openModal(title, url) {
    const frame = document.getElementById('crudModalFrame');
    document.getElementById('crudModalTitle').textContent = title;
    frame.style.height = '200px';
    frame.src = url;

    frame.onload = () => {
        const ajustarAltura = () => {
            try {
                const h = frame.contentDocument?.body?.scrollHeight;
                if (h) frame.style.height = h + 'px';
            } catch (_) { frame.style.height = '360px'; }
        };
        ajustarAltura();
        setTimeout(ajustarAltura, 150);
        setTimeout(ajustarAltura, 400);
    };

    document.getElementById('crudModal').classList.add('show');
}

function closeModal() {
    document.getElementById('crudModal').classList.remove('show');
    document.getElementById('crudModalFrame').src = '';
}

// ── Tabla ──────────────────────────────────────────────
function moduleByKey(key) {
    return modules.find(m => m.key === key) ?? modules[0];
}

function badgeClass(value) {
    const v = String(value).toLowerCase();
    if (['activo', 'conectado', 'online', 'realizado'].some(x => v.includes(x))) return 'badge badge-ok';
    if (['pendiente', 'sin'].some(x => v.includes(x)))              return 'badge badge-warn';
    if (['inactivo', 'anulado', 'error'].some(x => v.includes(x)))  return 'badge badge-danger';
    return '';
}

function estadoTexto(valor) {
    if (valor === undefined || valor === null) return '-';
    return valor === 1 ? 'Activo' : 'Inactivo';
}

function mapApiRows(module, data) {
    switch (module.key) {
        case 'productos': {
            const defaultImg = '/images/productos/no-imagen.svg';
            return data.map(item => {
                const src = item.imagen_Path || defaultImg;
                const imgHtml = `<img src="${src}" class="producto-thumb" alt="${item.nombre_Producto ?? ''}" onerror="this.src='${defaultImg}'">`;
                return {
                    id: item.id_Producto,
                    cells: [
                        imgHtml,
                        item.codigo ?? '-',
                        item.nombre_Producto ?? '-',
                        item.nombre_Proveedor ?? 'Sin proveedor',
                        `₡${(item.precio_Venta ?? 0).toLocaleString('es-CR')}`,
                        String(item.stock_Actual ?? 0),
                        estadoTexto(item.id_Estado)
                    ]
                };
            });
        }
        case 'proveedores':
            return data.map(item => ({
                id: item.id_Proveedor,
                cells: [
                    item.identificacion_Proveedor ?? '-',
                    item.nombre_Proveedor ?? '-',
                    item.telefono_Proveedor ?? '-',
                    item.correo_Proveedor ?? '-',
                    estadoTexto(item.id_Estado)
                ]
            }));
        case 'usuarios':
            return data.map(item => ({
                id: item.id_Usuario,
                cells: [
                    item.nombre_Usuario ?? '-',
                    item.nombre_Trabajador ?? '-',
                    estadoTexto(item.id_Estado)
                ]
            }));
        case 'planilla':
            return data.map(item => ({
                id: item.id_Trabajador,
                cells: [
                    item.cedula ?? '-',
                    item.nombre_Completo ?? '-',
                    item.nombre_Puesto ?? '-',
                    estadoTexto(item.id_Estado)
                ]
            }));
        case 'puestos':
            return data.map(item => ({
                id: item.id_Puesto,
                cells: [
                    item.nombre_Puesto ?? '-',
                    estadoTexto(item.id_Estado)
                ]
            }));
        case 'clientes':
            return data.map(item => ({
                id: item.id_Cliente,
                cells: [
                    item.cedula ?? '-',
                    item.nombre_Completo ?? '-',
                    item.correo_Cliente ?? '-',
                    item.telefono_Cliente ?? '-',
                    estadoTexto(item.id_Estado)
                ]
            }));
        case 'inventario':
            if (currentRole === 'Panadero') {
                return data
                    .filter(item => item.id_Estado === 1)
                    .map(item => ({
                        id: item.id_Inventario,
                        cells: [
                            item.nombre ?? '-',
                            item.unidad ?? '-',
                            String(item.stock_Actual ?? 0),
                            String(item.stock_Minimo ?? 0)
                        ]
                    }));
            }
            return data.map(item => ({
                id: item.id_Inventario,
                cells: [
                    item.nombre ?? '-',
                    item.unidad ?? '-',
                    String(item.stock_Actual ?? 0),
                    String(item.stock_Minimo ?? 0),
                    item.nombre_Proveedor ?? 'Sin proveedor',
                    estadoTexto(item.id_Estado)
                ]
            }));
        case 'produccion':
            return data.map(item => ({
                id: item.id_Asignacion,
                cells: [
                    item.nombre_Trabajador ?? '-',
                    item.nombre_Producto ?? '-',
                    String(item.cantidad_Diaria ?? 0),
                    item.realizado ? 'Realizado' : 'Pendiente',
                    'Activo'
                ]
            }));
        default:
            return (module.table.rows ?? []).map(cells => ({ id: null, cells }));
    }
}

function renderStockAlerts(data) {
    const existing = document.getElementById('stockAlertPanel');
    if (existing) existing.remove();

    const bajoMinimo = data.filter(i => (i.stock_Actual ?? 0) <= (i.stock_Minimo ?? 0) && i.stock_Minimo > 0);

    bajoMinimo.forEach(i => {
        NotificationManager.add(
            'warning',
            `Stock bajo: ${i.nombre}`,
            `Actual: ${i.stock_Actual} ${i.unidad ?? ''} — Mínimo: ${i.stock_Minimo}`,
            'stock'
        );
    });

    if (bajoMinimo.length === 0) return;

    const rows = bajoMinimo.map(i => `
        <div class="stock-alert-row">
            <span class="stock-alert-name">${i.nombre ?? '-'}</span>
            <span class="stock-alert-values">
                <span class="chip-stock">${i.stock_Actual} ${i.unidad ?? ''}</span>
                <span class="chip-min">mín. ${i.stock_Minimo}</span>
            </span>
        </div>
    `).join('');

    const panel = document.createElement('div');
    panel.id = 'stockAlertPanel';
    panel.className = 'stock-alert-panel';
    panel.innerHTML = `
        <div class="stock-alert-title">Stock bajo minimo</div>
        <div class="stock-alert-body">${rows}</div>
    `;

    document.getElementById('moduleHeader').appendChild(panel);
}

function esVistaCajero(module) {
    return module.key === 'productos' && currentRole === 'Cajas';
}

function renderIframeModule(url) {
    els.tableStatus.textContent = '';
    els.tableWrap.innerHTML = `<iframe src="${url}" class="module-embed-frame"></iframe>`;
}

async function fillTable(module) {
    if (module.key === 'compras') {
        renderIframeModule('/Compra/Historial');
        return;
    }

    if (module.key === 'tiquetes') {
        renderIframeModule('/Tiquete/Historial');
        return;
    }

    if (module.key === 'reportes') {
        renderIframeModule('/Reporte/Dashboard');
        return;
    }

    if (module.key === 'accesos') {
        renderIframeModule('/Acceso/Historial');
        return;
    }

    if (module.key === 'salarios') {
        renderIframeModule('/Planilla/Historial');
        return;
    }

    if (module.key === 'vencimientos') {
        renderIframeModule('/Perdida/Historial');
        return;
    }

    els.tableTitle.textContent = module.table.title;

    if (!module.table.sourceUrl) {
        els.tableStatus.textContent = 'Información local';
        renderTable(module, []);
        return;
    }

    document.getElementById('stockAlertPanel')?.remove();
    if (module.key === 'inventario') NotificationManager.clearSource('stock');
    els.tableStatus.textContent = 'Cargando...';

    try {
        const res = await fetch(module.table.sourceUrl);

        if (res.status === 204) {
            els.tableStatus.textContent = 'Sin registros';
            if (esVistaCajero(module)) { renderProductGrid([]); return; }
            renderTable(module, []);
            return;
        }

        if (!res.ok) throw new Error(`HTTP ${res.status}`);

        const data = await res.json();
        els.tableStatus.textContent = 'Actualizado';

        if (module.key === 'inventario' && currentRole === 'Admin') renderStockAlerts(data);

        if (esVistaCajero(module)) {
            renderProductGrid(data.filter(p => p.id_Estado === 1));
            return;
        }

        renderTable(module, mapApiRows(module, data));

    } catch {
        els.tableStatus.textContent = 'Error al cargar datos';
        if (esVistaCajero(module)) { renderProductGrid([]); return; }
        renderTable(module, []);
    }
}

let productosCajeroCompletos = [];

function renderProductGrid(productos) {
    productos.forEach(p => { catalogoProductos[p.id_Producto] = p; });
    productosCajeroCompletos = productos;

    els.tableWrap.innerHTML = `
        <div class="pos-buscador">
            <input type="text" id="posBuscador" class="mf-input" placeholder="Buscar por código o nombre..." autocomplete="off" />
        </div>
        <div id="posGridContenedor"></div>
    `;

    const inputBuscador = document.getElementById('posBuscador');
    inputBuscador.addEventListener('input', () => renderTarjetasProducto(productos, inputBuscador.value));

    renderTarjetasProducto(productos, '');
    inputBuscador.focus();
}

function renderTarjetasProducto(productos, filtro) {
    const defaultImg = '/images/productos/no-imagen.svg';
    const contenedor = document.getElementById('posGridContenedor');
    const texto = filtro.trim().toLowerCase();

    const filtrados = texto
        ? productos.filter(p =>
            (p.codigo ?? '').toLowerCase().includes(texto) ||
            (p.nombre_Producto ?? '').toLowerCase().includes(texto))
        : productos;

    if (!filtrados.length) {
        contenedor.innerHTML = `<div class="empty-state">${texto ? 'No se encontraron productos que coincidan con la búsqueda.' : 'No hay productos disponibles.'}</div>`;
        return;
    }

    const tarjetas = filtrados.map(p => {
        const src = p.imagen_Path || defaultImg;
        const sinStock = (p.stock_Actual ?? 0) <= 0;
        return `
            <div class="pos-card ${sinStock ? 'pos-card-sin-stock' : ''}" data-id="${p.id_Producto}" title="${sinStock ? 'Sin stock disponible' : 'Click para agregar al carrito'}">
                <div class="pos-card-img-wrap">
                    <img src="${src}" alt="${p.nombre_Producto ?? ''}" class="pos-card-img" onerror="this.src='${defaultImg}'">
                </div>
                <div class="pos-card-info">
                    ${p.codigo ? `<div class="pos-card-codigo">${p.codigo}</div>` : ''}
                    <div class="pos-card-nombre">${p.nombre_Producto ?? '-'}</div>
                    <div class="pos-card-precio">₡${(p.precio_Venta ?? 0).toLocaleString('es-CR')}</div>
                    <div class="pos-card-stock">${sinStock ? 'Sin stock' : `Stock: ${p.stock_Actual}`}</div>
                </div>
            </div>`;
    }).join('');

    contenedor.innerHTML = `<div class="pos-grid">${tarjetas}</div>`;

    if (currentRole === 'Cajas') {
        contenedor.querySelectorAll('.pos-card:not(.pos-card-sin-stock)').forEach(card => {
            card.addEventListener('click', () => agregarAlCarrito(card.dataset.id));
        });
    }
}

// ── Carrito de venta (Cajero) ──────────────────────────
const catalogoProductos = {};
let carrito = [];

function agregarAlCarrito(idProducto) {
    const producto = catalogoProductos[idProducto];
    if (!producto) return;

    const enCarrito = carrito.find(i => i.id === idProducto);
    const cantidadActual = enCarrito ? enCarrito.cantidad : 0;

    if (cantidadActual + 1 > (producto.stock_Actual ?? 0)) {
        showToast(`No hay más stock disponible de "${producto.nombre_Producto}".`, 'error');
        return;
    }

    if (enCarrito) {
        enCarrito.cantidad++;
    } else {
        carrito.push({
            id: idProducto,
            nombre: producto.nombre_Producto,
            precio: producto.precio_Venta,
            cantidad: 1
        });
    }

    renderCarritoFlotante();
    renderCarritoModal();
    showToast(`"${producto.nombre_Producto}" agregado al carrito.`, 'success');
}

function cambiarCantidadCarrito(idProducto, delta) {
    const item = carrito.find(i => i.id === idProducto);
    if (!item) return;

    const producto = catalogoProductos[idProducto];
    const nuevaCantidad = item.cantidad + delta;

    if (nuevaCantidad <= 0) {
        carrito = carrito.filter(i => i.id !== idProducto);
    } else if (producto && nuevaCantidad > (producto.stock_Actual ?? 0)) {
        showToast('No hay más stock disponible.', 'error');
        return;
    } else {
        item.cantidad = nuevaCantidad;
    }

    renderCarritoFlotante();
    renderCarritoModal();
}

function quitarDelCarrito(idProducto) {
    carrito = carrito.filter(i => i.id !== idProducto);
    renderCarritoFlotante();
    renderCarritoModal();
}

function totalCarrito() {
    return carrito.reduce((sum, i) => sum + (i.precio * i.cantidad), 0);
}

function renderCarritoFlotante() {
    const btn = document.getElementById('cartFloatButton');
    if (!btn) return;

    const cantidadTotal = carrito.reduce((sum, i) => sum + i.cantidad, 0);

    if (currentRole === 'Cajas' && cantidadTotal > 0) {
        btn.style.display = 'flex';
        document.getElementById('cartFloatCount').textContent = cantidadTotal;
        document.getElementById('cartFloatTotal').textContent = `₡${totalCarrito().toLocaleString('es-CR')}`;
    } else {
        btn.style.display = 'none';
    }
}

function renderCarritoModal() {
    const lista = document.getElementById('cartItemsList');
    if (!lista) return;

    if (carrito.length === 0) {
        lista.innerHTML = `<div class="empty-state">El carrito está vacío.</div>`;
    } else {
        lista.innerHTML = carrito.map(i => `
            <div class="cart-item-row">
                <div class="cart-item-nombre">${i.nombre}</div>
                <div class="cart-item-controls">
                    <button type="button" class="mf-btn mf-btn-secondary cart-qty-btn" data-id="${i.id}" data-delta="-1">−</button>
                    <span class="cart-item-cantidad">${i.cantidad}</span>
                    <button type="button" class="mf-btn mf-btn-secondary cart-qty-btn" data-id="${i.id}" data-delta="1">+</button>
                </div>
                <div class="cart-item-subtotal">₡${(i.precio * i.cantidad).toLocaleString('es-CR')}</div>
                <button type="button" class="mf-btn mf-btn-danger cart-quitar-btn" data-id="${i.id}">Quitar</button>
            </div>
        `).join('');

        lista.querySelectorAll('.cart-qty-btn').forEach(btn => {
            btn.addEventListener('click', () => cambiarCantidadCarrito(btn.dataset.id, parseInt(btn.dataset.delta, 10)));
        });
        lista.querySelectorAll('.cart-quitar-btn').forEach(btn => {
            btn.addEventListener('click', () => quitarDelCarrito(btn.dataset.id));
        });
    }

    document.getElementById('cartTotalMonto').textContent = `₡${totalCarrito().toLocaleString('es-CR')}`;
    document.getElementById('cartErrorBanner').style.display = 'none';
    document.getElementById('cartResultadoBox').style.display = 'none';
    document.getElementById('cartResultadoBox').innerHTML = '';
}

let clienteSeleccionado = null;

document.getElementById('cartFloatButton')?.addEventListener('click', () => {
    renderCarritoModal();
    document.getElementById('cartModal').classList.add('show');
});

document.getElementById('closeCartModal')?.addEventListener('click', () => {
    document.getElementById('cartModal').classList.remove('show');
});

document.getElementById('cartModal')?.addEventListener('click', e => {
    if (e.target === document.getElementById('cartModal')) {
        document.getElementById('cartModal').classList.remove('show');
    }
});

document.getElementById('cartVaciarBtn')?.addEventListener('click', () => {
    if (carrito.length === 0) return;
    carrito = [];
    clienteSeleccionado = null;
    document.getElementById('cartCedulaInput').value = '';
    document.getElementById('cartClienteResultado').textContent = '';
    renderCarritoFlotante();
    renderCarritoModal();
});

document.getElementById('cartBuscarClienteBtn')?.addEventListener('click', async () => {
    const cedula = document.getElementById('cartCedulaInput').value.trim();
    const resultadoEl = document.getElementById('cartClienteResultado');

    if (!cedula) {
        clienteSeleccionado = null;
        resultadoEl.textContent = 'Se emitirá a nombre de Receptor Genérico.';
        return;
    }

    try {
        const res = await fetch(`/Venta/BuscarCliente?cedula=${encodeURIComponent(cedula)}`);
        const data = await res.json();

        if (data.encontrado) {
            clienteSeleccionado = data.id;
            resultadoEl.textContent = `Cliente encontrado: ${data.nombre}`;
            resultadoEl.style.color = 'var(--pt-blue-800)';
        } else {
            clienteSeleccionado = null;
            resultadoEl.textContent = 'No se encontró un cliente con esa cédula. Se emitirá a nombre de Receptor Genérico.';
            resultadoEl.style.color = 'var(--pt-gold-600)';
        }
    } catch {
        clienteSeleccionado = null;
        resultadoEl.textContent = 'No se pudo buscar el cliente. Se emitirá a nombre de Receptor Genérico.';
    }
});

document.getElementById('cartConfirmarBtn')?.addEventListener('click', async () => {
    if (carrito.length === 0) {
        showToast('El carrito está vacío.', 'error');
        return;
    }

    const payload = {
        Id_Cliente: clienteSeleccionado,
        SimularFallo: document.getElementById('cartSimularFallo').checked,
        Detalles: carrito.map(i => ({
            Id_Producto: i.id,
            Cantidad: i.cantidad,
            Precio_Unitario: i.precio
        }))
    };

    const errorBanner = document.getElementById('cartErrorBanner');
    const resultadoBox = document.getElementById('cartResultadoBox');
    errorBanner.style.display = 'none';
    resultadoBox.style.display = 'none';

    try {
        const res = await fetch('/Venta/Cobrar', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(payload)
        });
        const data = await res.json();

        if (!data.ok) {
            errorBanner.textContent = data.mensaje || 'No se pudo completar la venta.';
            errorBanner.style.display = 'block';
            return;
        }

        const estadoTexto = data.estado === 'Emitido' ? 'Emitido correctamente' : 'Guardado localmente (pendiente de envío a Hacienda)';
        resultadoBox.innerHTML = `
            <div class="cart-resultado-ok">
                <p><strong>Venta registrada.</strong></p>
                <p>Consecutivo: ${data.consecutivo}</p>
                <p>Estado: ${estadoTexto}</p>
                <p>Cliente: ${data.nombreCliente}</p>
                <p>Total: ₡${(data.montoTotal ?? 0).toLocaleString('es-CR')}</p>
                <div style="display:flex; gap:8px; margin-top:8px;">
                    <a href="/Tiquete/Detalle/${data.idTiquete}" target="_blank" class="mf-btn mf-btn-secondary">Ver recibo</a>
                    ${data.estado !== 'Emitido' ? `<button type="button" class="mf-btn mf-btn-primary" id="cartReintentarBtn" data-id="${data.idTiquete}">Reintentar envío</button>` : ''}
                </div>
            </div>
        `;
        resultadoBox.style.display = 'block';

        document.getElementById('cartReintentarBtn')?.addEventListener('click', async () => {
            const id = document.getElementById('cartReintentarBtn').dataset.id;
            const r = await fetch(`/Venta/Reintentar?idTiquete=${id}`, { method: 'POST' });
            const rData = await r.json();
            showToast(rData.ok ? 'Envío reintentado correctamente.' : (rData.mensaje || 'No se pudo reintentar.'), rData.ok ? 'success' : 'error');
            if (rData.ok) document.getElementById('cartReintentarBtn').remove();
        });

        carrito = [];
        clienteSeleccionado = null;
        document.getElementById('cartCedulaInput').value = '';
        document.getElementById('cartClienteResultado').textContent = '';
        renderCarritoFlotante();
        showToast('Venta registrada correctamente.', 'success');

        // Refresca el catalogo para reflejar el stock ya descontado
        if (state.module) await fillTable(state.module);

    } catch {
        errorBanner.textContent = 'No se pudo comunicar con el servidor. Intenta de nuevo.';
        errorBanner.style.display = 'block';
    }
});

function renderTable(module, rows) {
    currentRows = rows;
    selectedStatus = 'activo';
    currentPage = 1;
    renderPagedTable(module);
}

function applyStatusFilter(module) {
    const soloLectura = module && module.key === 'inventario' && currentRole === 'Panadero';

    filteredRows = (soloLectura || selectedStatus === 'todos')
        ? currentRows
        : currentRows.filter(row =>
            row.cells.some(cell => String(cell).toLowerCase() === selectedStatus)
          );
}

function renderPagedTable(module) {
    applyStatusFilter(module);

    const start      = (currentPage - 1) * pageSize;
    const pageRows   = filteredRows.slice(start, start + pageSize);
    const totalPages = Math.max(1, Math.ceil(filteredRows.length / pageSize));
    const soloLectura = module.key === 'inventario' && currentRole === 'Panadero';
    const baseUrl    = soloLectura ? null : crudRoutes[module.key];

    // columnas de puestos no incluye ID en la vista
    const columns = module.key === 'puestos'
        ? ['Puesto', 'Estado']
        : soloLectura
            ? ['Nombre', 'Unidad', 'Stock actual', 'Stock mínimo']
            : module.table.columns;

    const head = columns.map(c => `<th>${c}</th>`).join('');
    const actionsHead = baseUrl ? '<th>Acciones</th>' : '';

    const body = pageRows.map(row => {
        const cells = row.cells.map(cell => {
            const cls = badgeClass(cell);
            return `<td>${cls ? `<span class="${cls}">${cell}</span>` : cell}</td>`;
        }).join('');

        const activo = row.cells.some(c => String(c).toLowerCase() === 'activo');
        const esInventario = module.key === 'inventario';
        const esProduccion = module.key === 'produccion';
        const esPlanilla = module.key === 'planilla';
        const actionsCell = baseUrl && row.id ? `
            <td>
                <button class="row-btn row-btn-edit" data-id="${row.id}" data-module="${module.key}">Editar</button>
                ${esInventario ? `<button class="row-btn row-btn-movimiento" data-id="${row.id}" data-module="${module.key}">Registrar movimiento</button>` : ''}
                ${esInventario ? `<button class="row-btn row-btn-historial" data-id="${row.id}" data-module="${module.key}">Ver historial</button>` : ''}
                ${esProduccion ? `<button class="row-btn row-btn-imprimir" data-id="${row.id}">Imprimir receta</button>` : ''}
                ${esPlanilla ? `<button class="row-btn row-btn-pago" data-id="${row.id}">Configurar pago</button>` : ''}
                ${esPlanilla ? `<button class="row-btn row-btn-vacaciones" data-id="${row.id}">Vacaciones</button>` : ''}
                ${esPlanilla ? `<button class="row-btn row-btn-asistencia" data-id="${row.id}">Asistencia</button>` : ''}
                ${esPlanilla ? `<button class="row-btn row-btn-prestamos" data-id="${row.id}">Préstamos</button>` : ''}
                ${esPlanilla ? `<button class="row-btn row-btn-horasextra" data-id="${row.id}">Horas extra</button>` : ''}
                ${esPlanilla ? `<button class="row-btn row-btn-detallepago" data-id="${row.id}">Generar detalle de pago</button>` : ''}
                ${activo
                    ? `<button class="row-btn row-btn-delete" data-id="${row.id}" data-module="${module.key}" data-activo="true">Desactivar registro</button>`
                    : `<button class="row-btn row-btn-activate" data-id="${row.id}" data-module="${module.key}" data-activo="false">Reactivar registro</button>`
                }
            </td>` : '';

        return `<tr>${cells}${actionsCell}</tr>`;
    }).join('');

    els.tableWrap.innerHTML = `
        ${soloLectura ? '' : `
        <div class="table-tools">
            <select id="statusFilter">
                <option value="todos">Todos</option>
                <option value="activo">Activos</option>
                <option value="inactivo">Inactivos</option>
            </select>
        </div>`}

        ${filteredRows.length
            ? `<table class="data-table">
                <thead><tr>${head}${actionsHead}</tr></thead>
                <tbody>${body}</tbody>
               </table>`
            : `<div class="empty-state">${module.table.emptyMessage ?? 'Sin datos.'}</div>`
        }

        <div class="pagination">
            <button id="prevPage" ${currentPage === 1 ? 'disabled' : ''}>Anterior</button>
            <span>Página ${currentPage} de ${totalPages}</span>
            <button id="nextPage" ${currentPage === totalPages ? 'disabled' : ''}>Siguiente</button>
        </div>
    `;

    const statusFilterEl = document.getElementById('statusFilter');
    if (statusFilterEl) statusFilterEl.value = selectedStatus;
    statusFilterEl?.addEventListener('change', e => {
        selectedStatus = e.target.value;
        currentPage = 1;
        renderPagedTable(module);
    });
    document.getElementById('prevPage')?.addEventListener('click', () => {
        if (currentPage > 1) { currentPage--; renderPagedTable(module); }
    });
    document.getElementById('nextPage')?.addEventListener('click', () => {
        if (currentPage < totalPages) { currentPage++; renderPagedTable(module); }
    });

    els.tableWrap.querySelectorAll('.row-btn-movimiento').forEach(btn => {
        btn.addEventListener('click', () => {
            openModal('Registrar Movimiento', `${crudRoutes[btn.dataset.module]}/Movimiento/${btn.dataset.id}?modal=true`);
        });
    });

    els.tableWrap.querySelectorAll('.row-btn-historial').forEach(btn => {
        btn.addEventListener('click', () => {
            openModal('Historial de Movimientos', `${crudRoutes[btn.dataset.module]}/Historial/${btn.dataset.id}?modal=true`);
        });
    });

    els.tableWrap.querySelectorAll('.row-btn-imprimir').forEach(btn => {
        btn.addEventListener('click', () => {
            window.open(`/Produccion/Imprimir/${btn.dataset.id}`, '_blank');
        });
    });

    els.tableWrap.querySelectorAll('.row-btn-pago').forEach(btn => {
        btn.addEventListener('click', () => {
            openModal('Configurar Tipo de Pago', `/Trabajador/ConfigurarPago/${btn.dataset.id}?modal=true`);
        });
    });

    els.tableWrap.querySelectorAll('.row-btn-vacaciones').forEach(btn => {
        btn.addEventListener('click', () => {
            window.open(`/Trabajador/Vacaciones/${btn.dataset.id}`, '_blank');
        });
    });

    els.tableWrap.querySelectorAll('.row-btn-asistencia').forEach(btn => {
        btn.addEventListener('click', () => {
            window.open(`/Trabajador/Asistencia/${btn.dataset.id}`, '_blank');
        });
    });

    els.tableWrap.querySelectorAll('.row-btn-prestamos').forEach(btn => {
        btn.addEventListener('click', () => {
            window.open(`/Trabajador/Prestamos/${btn.dataset.id}`, '_blank');
        });
    });

    els.tableWrap.querySelectorAll('.row-btn-horasextra').forEach(btn => {
        btn.addEventListener('click', () => {
            window.open(`/Trabajador/HorasExtra/${btn.dataset.id}`, '_blank');
        });
    });

    els.tableWrap.querySelectorAll('.row-btn-detallepago').forEach(btn => {
        btn.addEventListener('click', () => {
            openModal('Generar Detalle de Pago', `/Trabajador/GenerarDetallePago/${btn.dataset.id}?modal=true`);
        });
    });

    els.tableWrap.querySelectorAll('.row-btn-edit').forEach(btn => {
        btn.addEventListener('click', () => {
            openModal(`Editar ${module.name}`, `${crudRoutes[btn.dataset.module]}/Editar/${btn.dataset.id}?modal=true`);
        });
    });

    els.tableWrap.querySelectorAll('.row-btn-delete').forEach(btn => {
        btn.addEventListener('click', async () => {
            const ok = await showConfirm(`¿Estás seguro de que deseas desactivar este registro de ${module.name}?`, 'Sí, desactivar');
            if (!ok) return;

            try {
                await fetch(`${crudRoutes[btn.dataset.module]}/Eliminar/${btn.dataset.id}`);
                showToast(`Registro de ${module.name} desactivado correctamente.`, 'success');
            } catch {
                showToast('No se pudo desactivar el registro. Intenta de nuevo.', 'error');
            }

            await fillTable(module);
        });
    });

    els.tableWrap.querySelectorAll('.row-btn-activate').forEach(btn => {
        btn.addEventListener('click', async () => {
            const ok = await showConfirm(`¿Deseas reactivar este registro de ${module.name}?`, 'Sí, activar', 'activate');
            if (!ok) return;

            try {
                await fetch(`${crudRoutes[btn.dataset.module]}/Activar/${btn.dataset.id}`);
                showToast(`Registro de ${module.name} activado correctamente.`, 'success');
            } catch {
                showToast('No se pudo activar el registro. Intenta de nuevo.', 'error');
            }

            await fillTable(module);
        });
    });
}

async function renderModule(key) {
    const module = moduleByKey(key);
    state.selected = module.key;
    state.module   = module;

    document.querySelectorAll('.sidebar-item').forEach(btn => {
        btn.classList.toggle('active', btn.dataset.module === module.key);
    });
    abrirGrupoDelModulo(module.key);

    const soloLectura = (module.key === 'inventario' && currentRole === 'Panadero') || esVistaCajero(module) || module.key === 'compras' || module.key === 'tiquetes' || module.key === 'reportes' || module.key === 'accesos' || module.key === 'salarios' || module.key === 'vencimientos';
    const baseUrl = soloLectura ? null : crudRoutes[module.key];
    if (baseUrl && els.createButton) {
        els.createButton.textContent = 'Agregar';
        els.createButton.onclick = () => openModal(`Agregar ${module.name}`, `${baseUrl}/Crear?modal=true`);
        els.createButton.style.display = 'inline-flex';
    } else if (els.createButton) {
        els.createButton.style.display = 'none';
    }

    const imprimirListaBtn = document.getElementById('imprimirListaButton');
    if (imprimirListaBtn) {
        if (module.key === 'produccion' && currentRole === 'Admin') {
            imprimirListaBtn.style.display = 'inline-flex';
            imprimirListaBtn.onclick = () => openModal('Imprimir Lista de Producción', '/Produccion/SeleccionarListaEmpleado?modal=true');
        } else {
            imprimirListaBtn.style.display = 'none';
        }
    }

    els.title.textContent       = module.name;
    els.tag.textContent         = module.tag;
    els.description.textContent = module.description;

    await fillTable(module);
}

// ── Eventos globales ───────────────────────────────────
els.sidebar.addEventListener('click', e => {
    const headerBtn = e.target.closest('.sidebar-group-header');
    if (headerBtn) {
        headerBtn.closest('.sidebar-group').classList.toggle('open');
        return;
    }

    const btn = e.target.closest('[data-module]');
    if (!btn) return;

    if (btn.dataset.module === 'mi_produccion') {
        window.location.href = '/Produccion/MiLista';
        return;
    }

    renderModule(btn.dataset.module);
});

function abrirGrupoDelModulo(key) {
    const btn = els.sidebar.querySelector(`[data-module="${key}"]`);
    const grupo = btn?.closest('.sidebar-group');
    grupo?.classList.add('open');
}

sidebarUi.toggle?.addEventListener('click', () => {
    sidebarHidden = !sidebarHidden;
    persistSidebarState();
    applySidebarState();
});

sidebarUi.backdrop?.addEventListener('click', () => {
    sidebarHidden = true;
    persistSidebarState();
    applySidebarState();
});

document.getElementById('closeCrudModal')?.addEventListener('click', closeModal);

document.getElementById('crudModal')?.addEventListener('click', e => {
    if (e.target === document.getElementById('crudModal')) closeModal();
});

window.addEventListener('message', async e => {
    if (e.data === 'crud-success' && state.module) {
        closeModal();
        await fillTable(state.module);
        showToast(`${state.module.name} guardado correctamente.`, 'success');
    }
});

async function cargarNotificacionesPanadero() {
    if (currentRole !== 'Panadero' || !currentIdTrabajador) return;

    try {
        const res = await fetch(`${apiBaseUrl}api/Produccion/lista-diaria?Id_Trabajador=${currentIdTrabajador}`);
        if (res.status === 204) return;
        if (!res.ok) return;

        const data = await res.json();
        const pendientes = data.filter(item => !item.realizado);

        if (pendientes.length > 0) {
            NotificationManager.add(
                'info',
                'Producción pendiente',
                `Tienes ${pendientes.length} producción(es) por realizar hoy.`,
                'produccion'
            );
        }
    } catch { /* sin notificacion si falla */ }
}

async function cargarAlertaVencimientos() {
    if (currentRole !== 'Admin') return;

    try {
        const res = await fetch(`${apiBaseUrl}api/Perdida/pendientes`);
        if (res.status === 204) return;
        if (!res.ok) return;

        const data = await res.json();

        if (data.length > 0) {
            NotificationManager.add(
                'warning',
                'Productos vencidos',
                `Hay ${data.length} lote(s) de inventario vencidos pendientes de procesar como pérdida.`,
                'vencimientos'
            );
        }
    } catch { /* sin notificacion si falla */ }
}

function mostrarBienvenida() {
    state.selected = null;
    state.module = null;

    document.querySelectorAll('.sidebar-item').forEach(btn => btn.classList.remove('active'));

    if (els.createButton) els.createButton.style.display = 'none';
    const imprimirListaBtn = document.getElementById('imprimirListaButton');
    if (imprimirListaBtn) imprimirListaBtn.style.display = 'none';

    const nombreUsuario = document.querySelector('.avatar-dropdown-name')?.textContent?.trim() ?? '';
    const fechaHoy = new Date().toLocaleDateString('es-CR', { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' });
    const rolTexto = { Admin: 'Administrador', Cajas: 'Cajero', Panadero: 'Panadero' }[currentRole] ?? currentRole;

    els.title.textContent = 'Bienvenido a Punto Trigo';
    els.tag.textContent = '';
    els.description.textContent = '';
    els.tableTitle.textContent = '';
    els.tableStatus.textContent = '';

    els.tableWrap.innerHTML = `
        <div class="welcome-screen">
            <div class="welcome-icon">🥖</div>
            <div class="welcome-title">Hola, ${nombreUsuario}</div>
            <div class="welcome-subtitle">Selecciona un módulo del menú para comenzar a trabajar.</div>
            <div class="welcome-cards">
                <div class="welcome-card">
                    <div class="welcome-card-label">Rol</div>
                    <div class="welcome-card-value">${rolTexto}</div>
                </div>
                <div class="welcome-card">
                    <div class="welcome-card-label">Fecha</div>
                    <div class="welcome-card-value" style="text-transform:capitalize;">${fechaHoy}</div>
                </div>
            </div>
        </div>
    `;
}

document.getElementById('brandHomeLink')?.addEventListener('click', () => {
    mostrarBienvenida();
});

applySidebarState();
mostrarBienvenida();
cargarNotificacionesPanadero();
cargarAlertaVencimientos();

// ── Avatar dropdown ────────────────────────────────
document.getElementById('avatarBtn')?.addEventListener('click', e => {
    e.stopPropagation();
    document.getElementById('avatarDropdown').classList.toggle('open');
});

document.addEventListener('click', e => {
    if (!document.getElementById('avatarWrapper')?.contains(e.target))
        document.getElementById('avatarDropdown')?.classList.remove('open');
});
