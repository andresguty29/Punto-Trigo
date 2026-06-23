// ── NotificationManager ────────────────────────────
const NotificationManager = (() => {
    const ICONS = { warning: '⚠', error: '✕', info: 'ℹ' };
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

const modules = JSON.parse(
    document.getElementById("dashboard-data").textContent
);

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
    inventario: '/Inventario'
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
        try {
            const h = frame.contentDocument?.body?.scrollHeight;
            if (h) frame.style.height = h + 'px';
        } catch (_) { frame.style.height = '360px'; }
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
    if (['activo', 'conectado', 'online'].some(x => v.includes(x))) return 'badge badge-ok';
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
        case 'productos':
            return data.map(item => ({
                id: item.id_Producto,
                cells: [
                    item.nombre_Producto ?? '-',
                    item.nombre_Proveedor ?? 'Sin proveedor',
                    `₡${(item.precio_Venta ?? 0).toLocaleString('es-CR')}`,
                    String(item.stock_Actual ?? 0),
                    estadoTexto(item.id_Estado)
                ]
            }));
        case 'proveedores':
            return data.map(item => ({
                id: item.id_Proveedor,
                cells: [
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
        case 'inventario':
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
        <div class="stock-alert-title">⚠ Stock bajo mínimo</div>
        <div class="stock-alert-body">${rows}</div>
    `;

    document.getElementById('moduleHeader').appendChild(panel);
}

async function fillTable(module) {
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
            renderTable(module, []);
            return;
        }

        if (!res.ok) throw new Error(`HTTP ${res.status}`);

        const data = await res.json();
        els.tableStatus.textContent = 'Actualizado';

        if (module.key === 'inventario') renderStockAlerts(data);

        renderTable(module, mapApiRows(module, data));

    } catch {
        els.tableStatus.textContent = 'Error al cargar datos';
        renderTable(module, []);
    }
}

function renderTable(module, rows) {
    currentRows = rows;
    selectedStatus = 'activo';
    currentPage = 1;
    renderPagedTable(module);
}

function applyStatusFilter() {
    filteredRows = selectedStatus === 'todos'
        ? currentRows
        : currentRows.filter(row =>
            row.cells.some(cell => String(cell).toLowerCase() === selectedStatus)
          );
}

function renderPagedTable(module) {
    applyStatusFilter();

    const start      = (currentPage - 1) * pageSize;
    const pageRows   = filteredRows.slice(start, start + pageSize);
    const totalPages = Math.max(1, Math.ceil(filteredRows.length / pageSize));
    const baseUrl    = crudRoutes[module.key];

    // columnas de puestos no incluye ID en la vista
    const columns = module.key === 'puestos'
        ? ['Puesto', 'Estado']
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
        const actionsCell = baseUrl && row.id ? `
            <td>
                <button class="row-btn row-btn-edit" data-id="${row.id}" data-module="${module.key}">Editar</button>
                ${esInventario ? `<button class="row-btn row-btn-movimiento" data-id="${row.id}" data-module="${module.key}">Movimiento</button>` : ''}
                ${activo
                    ? `<button class="row-btn row-btn-delete" data-id="${row.id}" data-module="${module.key}" data-activo="true">Desactivar</button>`
                    : `<button class="row-btn row-btn-activate" data-id="${row.id}" data-module="${module.key}" data-activo="false">Activar</button>`
                }
            </td>` : '';

        return `<tr>${cells}${actionsCell}</tr>`;
    }).join('');

    els.tableWrap.innerHTML = `
        <div class="table-tools">
            <select id="statusFilter">
                <option value="todos">Todos</option>
                <option value="activo">Activos</option>
                <option value="inactivo">Inactivos</option>
            </select>
        </div>

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

    document.getElementById('statusFilter').value = selectedStatus;
    document.getElementById('statusFilter')?.addEventListener('change', e => {
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

    const baseUrl = crudRoutes[module.key];
    if (baseUrl && els.createButton) {
        els.createButton.textContent = 'Agregar';
        els.createButton.onclick = () => openModal(`Agregar ${module.name}`, `${baseUrl}/Crear?modal=true`);
        els.createButton.style.display = 'inline-flex';
    } else if (els.createButton) {
        els.createButton.style.display = 'none';
    }

    els.title.textContent       = module.name;
    els.tag.textContent         = module.tag;
    els.description.textContent = module.description;

    await fillTable(module);
}

// ── Eventos globales ───────────────────────────────────
els.sidebar.addEventListener('click', e => {
    const btn = e.target.closest('[data-module]');
    if (btn) renderModule(btn.dataset.module);
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

renderModule(state.selected);

// ── Avatar dropdown ────────────────────────────────
document.getElementById('avatarBtn')?.addEventListener('click', e => {
    e.stopPropagation();
    document.getElementById('avatarDropdown').classList.toggle('open');
});

document.addEventListener('click', e => {
    if (!document.getElementById('avatarWrapper')?.contains(e.target))
        document.getElementById('avatarDropdown')?.classList.remove('open');
});
