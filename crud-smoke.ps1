$ErrorActionPreference = 'Stop'

$base = 'http://localhost:21153/api'
$connectionString = 'Server=localhost;Database=PanaderiaDB;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=False'
$suffix = [DateTime]::UtcNow.ToString('yyyyMMddHHmmss')

$tracked = [ordered]@{
    puestoStandalone = $null
    puestoDependencia = $null
    trabajador = $null
    usuario = $null
    proveedor = $null
    producto = $null
}

$results = New-Object System.Collections.Generic.List[string]
$failures = New-Object System.Collections.Generic.List[string]

function Get-JsonResponse {
    param(
        [string]$Method,
        [string]$Url,
        [object]$Body = $null
    )

    $params = @{
        Method = $Method
        Uri = $Url
        Headers = @{ Accept = 'application/json' }
        UseBasicParsing = $true
    }

    if ($null -ne $Body) {
        $params['ContentType'] = 'application/json'
        $params['Body'] = ($Body | ConvertTo-Json -Depth 10)
    }

    Invoke-WebRequest @params
}

function Extract-IdFromLocation {
    param([string]$Location)

    $match = [regex]::Match($Location, '[0-9a-fA-F-]{36}$')
    if (-not $match.Success) {
        throw "No se pudo extraer GUID desde Location: $Location"
    }

    $match.Value
}

function Convert-GuidToInt {
    param([string]$GuidValue)

    [Convert]::ToInt32($GuidValue.Substring($GuidValue.Length - 8), 16)
}

function Assert-Status {
    param(
        $Response,
        [int[]]$Expected,
        [string]$Step
    )

    if ($Expected -notcontains [int]$Response.StatusCode) {
        throw "$Step devolvio estado $($Response.StatusCode) y se esperaba $($Expected -join ',')"
    }
}

function Add-Pass {
    param([string]$Message)
    $script:results.Add("OK  | $Message") | Out-Null
}

function Add-Fail {
    param([string]$Message)
    $script:failures.Add("FAIL| $Message") | Out-Null
}

function Invoke-DbNonQuery {
    param([string]$Sql)

    $cn = New-Object System.Data.SqlClient.SqlConnection $script:connectionString
    try {
        $cn.Open()
        $cmd = $cn.CreateCommand()
        $cmd.CommandText = $Sql
        [void]$cmd.ExecuteNonQuery()
    }
    finally {
        $cn.Dispose()
    }
}

function Cleanup-TestData {
    $deleteStatements = New-Object System.Collections.Generic.List[string]

    if ($script:tracked['usuario']) {
        $deleteStatements.Add("DELETE FROM dbo.USUARIO_TB WHERE ID_USUARIO = $(Convert-GuidToInt $script:tracked['usuario']);") | Out-Null
    }
    if ($script:tracked['trabajador']) {
        $deleteStatements.Add("DELETE FROM dbo.TRABAJADOR_TB WHERE ID_TRABAJADOR = $(Convert-GuidToInt $script:tracked['trabajador']);") | Out-Null
    }
    if ($script:tracked['puestoDependencia']) {
        $deleteStatements.Add("DELETE FROM dbo.PUESTO_TB WHERE ID_PUESTO = $(Convert-GuidToInt $script:tracked['puestoDependencia']);") | Out-Null
    }
    if ($script:tracked['puestoStandalone']) {
        $deleteStatements.Add("DELETE FROM dbo.PUESTO_TB WHERE ID_PUESTO = $(Convert-GuidToInt $script:tracked['puestoStandalone']);") | Out-Null
    }
    if ($script:tracked['proveedor']) {
        $deleteStatements.Add("DELETE FROM dbo.PROVEEDOR_TB WHERE ID_PROVEEDOR = $(Convert-GuidToInt $script:tracked['proveedor']);") | Out-Null
    }
    if ($script:tracked['producto']) {
        $deleteStatements.Add("DELETE FROM dbo.PRODUCTO_TB WHERE ID_PRODUCTO = $(Convert-GuidToInt $script:tracked['producto']);") | Out-Null
    }

    if ($deleteStatements.Count -gt 0) {
        Invoke-DbNonQuery ($deleteStatements -join [Environment]::NewLine)
    }
}

function Run-Step {
    param(
        [string]$Name,
        [scriptblock]$Action
    )

    try {
        & $Action
    }
    catch {
        Add-Fail ("$Name -> $($_.Exception.Message)")
    }
}

try {
    Run-Step 'Puesto standalone CRUD' {
        $post = Get-JsonResponse -Method 'POST' -Url "$base/puesto" -Body @{ nombre_Puesto = "QA Puesto Solo $suffix" }
        Assert-Status $post @(201) 'POST puesto standalone'
        $script:tracked['puestoStandalone'] = Extract-IdFromLocation $post.Headers.Location
        Add-Pass ("POST /puesto creo $($script:tracked['puestoStandalone'])")

        $get = Get-JsonResponse -Method 'GET' -Url "$base/puesto/$($script:tracked['puestoStandalone'])"
        Assert-Status $get @(200) 'GET puesto standalone'
        if (($get.Content | ConvertFrom-Json).nombre_Puesto -ne "QA Puesto Solo $suffix") {
            throw 'GET puesto standalone devolvio datos inesperados'
        }
        Add-Pass('GET /puesto/{id} devuelve el puesto creado')

        $put = Get-JsonResponse -Method 'PUT' -Url "$base/puesto/$($script:tracked['puestoStandalone'])" -Body @{ nombre_Puesto = "QA Puesto Solo Editado $suffix" }
        Assert-Status $put @(200) 'PUT puesto standalone'
        Add-Pass('PUT /puesto/{id} actualiza un puesto sin dependencias')

        $delete = Get-JsonResponse -Method 'DELETE' -Url "$base/puesto/$($script:tracked['puestoStandalone'])"
        Assert-Status $delete @(204) 'DELETE puesto standalone'
        Add-Pass('DELETE /puesto/{id} funciona cuando no hay trabajadores asociados')

        $script:tracked['puestoStandalone'] = $null
    }

    Run-Step 'Producto CRUD' {
        $post = Get-JsonResponse -Method 'POST' -Url "$base/producto" -Body @{ nombre_Producto = "Producto CRUD $suffix"; precio_Venta = 1250.50; stock_Actual = 11 }
        Assert-Status $post @(201) 'POST producto'
        $script:tracked['producto'] = Extract-IdFromLocation $post.Headers.Location
        Add-Pass ("POST /producto creo $($script:tracked['producto'])")

        $get = Get-JsonResponse -Method 'GET' -Url "$base/producto/$($script:tracked['producto'])"
        Assert-Status $get @(200) 'GET producto'
        if (($get.Content | ConvertFrom-Json).nombre_Producto -ne "Producto CRUD $suffix") {
            throw 'GET producto devolvio datos inesperados'
        }
        Add-Pass('GET /producto/{id} devuelve el producto creado')

        $put = Get-JsonResponse -Method 'PUT' -Url "$base/producto/$($script:tracked['producto'])" -Body @{ nombre_Producto = "Producto CRUD Editado $suffix"; precio_Venta = 1350.75; stock_Actual = 9 }
        Assert-Status $put @(200) 'PUT producto'
        Add-Pass('PUT /producto/{id} actualiza precio y stock')

        $delete = Get-JsonResponse -Method 'DELETE' -Url "$base/producto/$($script:tracked['producto'])"
        Assert-Status $delete @(204) 'DELETE producto'
        Add-Pass('DELETE /producto/{id} responde 204 con borrado logico')
    }

    Run-Step 'Proveedor CRUD' {
        $post = Get-JsonResponse -Method 'POST' -Url "$base/proveedor" -Body @{ nombre_Proveedor = "Proveedor CRUD $suffix"; telefono_Proveedor = '8888-9999'; correo_Proveedor = "proveedor.$suffix@test.local" }
        Assert-Status $post @(201) 'POST proveedor'
        $script:tracked['proveedor'] = Extract-IdFromLocation $post.Headers.Location
        Add-Pass ("POST /proveedor creo $($script:tracked['proveedor'])")

        $get = Get-JsonResponse -Method 'GET' -Url "$base/proveedor/$($script:tracked['proveedor'])"
        Assert-Status $get @(200) 'GET proveedor'
        if (($get.Content | ConvertFrom-Json).nombre_Proveedor -ne "Proveedor CRUD $suffix") {
            throw 'GET proveedor devolvio datos inesperados'
        }
        Add-Pass('GET /proveedor/{id} devuelve el proveedor creado')

        $put = Get-JsonResponse -Method 'PUT' -Url "$base/proveedor/$($script:tracked['proveedor'])" -Body @{ nombre_Proveedor = "Proveedor CRUD Editado $suffix"; telefono_Proveedor = '8888-1111'; correo_Proveedor = "proveedor.edit.$suffix@test.local" }
        Assert-Status $put @(200) 'PUT proveedor'
        Add-Pass('PUT /proveedor/{id} actualiza nombre, telefono y correo')

        $delete = Get-JsonResponse -Method 'DELETE' -Url "$base/proveedor/$($script:tracked['proveedor'])"
        Assert-Status $delete @(204) 'DELETE proveedor'
        Add-Pass('DELETE /proveedor/{id} responde 204 con borrado logico')
    }

    Run-Step 'Puesto + Trabajador + Usuario' {
        $puestoPost = Get-JsonResponse -Method 'POST' -Url "$base/puesto" -Body @{ nombre_Puesto = "QA Puesto Dependencia $suffix" }
        Assert-Status $puestoPost @(201) 'POST puesto dependencia'
        $script:tracked['puestoDependencia'] = Extract-IdFromLocation $puestoPost.Headers.Location
        Add-Pass ("POST /puesto para dependencias creo $($script:tracked['puestoDependencia'])")

        $trabajadorPost = Get-JsonResponse -Method 'POST' -Url "$base/trabajador" -Body @{ cedula = "TEST-$suffix"; nombre_Completo = "Trabajador CRUD $suffix"; id_Puesto = $script:tracked['puestoDependencia'] }
        Assert-Status $trabajadorPost @(201) 'POST trabajador'
        $script:tracked['trabajador'] = Extract-IdFromLocation $trabajadorPost.Headers.Location
        Add-Pass ("POST /trabajador creo $($script:tracked['trabajador'])")

        $trabajadorGet = Get-JsonResponse -Method 'GET' -Url "$base/trabajador/$($script:tracked['trabajador'])"
        Assert-Status $trabajadorGet @(200) 'GET trabajador'
        if (($trabajadorGet.Content | ConvertFrom-Json).nombre_Completo -ne "Trabajador CRUD $suffix") {
            throw 'GET trabajador devolvio datos inesperados'
        }
        Add-Pass('GET /trabajador/{id} devuelve el trabajador creado')

        $trabajadorPut = Get-JsonResponse -Method 'PUT' -Url "$base/trabajador/$($script:tracked['trabajador'])" -Body @{ cedula = "TEST-$suffix-EDIT"; nombre_Completo = "Trabajador CRUD Editado $suffix"; id_Puesto = $script:tracked['puestoDependencia'] }
        Assert-Status $trabajadorPut @(200) 'PUT trabajador'
        Add-Pass('PUT /trabajador/{id} actualiza cedula y nombre')

        $usuarioPost = Get-JsonResponse -Method 'POST' -Url "$base/usuario" -Body @{ nombre_Usuario = "qa_user_$suffix"; contrasena = 'ClaveTemporal123'; id_Trabajador = $script:tracked['trabajador'] }
        Assert-Status $usuarioPost @(201) 'POST usuario'
        $script:tracked['usuario'] = Extract-IdFromLocation $usuarioPost.Headers.Location
        Add-Pass ("POST /usuario creo $($script:tracked['usuario'])")

        $usuarioGet = Get-JsonResponse -Method 'GET' -Url "$base/usuario/$($script:tracked['usuario'])"
        Assert-Status $usuarioGet @(200) 'GET usuario'
        $usuario = $usuarioGet.Content | ConvertFrom-Json
        if ($usuario.nombre_Usuario -ne "qa_user_$suffix") {
            throw 'GET usuario devolvio nombre inesperado'
        }
        Add-Pass('GET /usuario/{id} devuelve el usuario creado')

        if ($usuario.id_Trabajador -ne $script:tracked['trabajador']) {
            Add-Fail('GET /usuario/{id} no preserva Id_Trabajador; el contrato API no coincide con la tabla real')
        }
        else {
            Add-Pass('GET /usuario/{id} preserva Id_Trabajador')
        }

        $usuarioPut = Get-JsonResponse -Method 'PUT' -Url "$base/usuario/$($script:tracked['usuario'])" -Body @{ nombre_Usuario = "qa_user_edit_$suffix"; contrasena = 'ClaveTemporal1234'; id_Trabajador = $script:tracked['trabajador'] }
        Assert-Status $usuarioPut @(200) 'PUT usuario'
        Add-Pass('PUT /usuario/{id} actualiza usuario y contraseña')

        $usuarioDelete = Get-JsonResponse -Method 'DELETE' -Url "$base/usuario/$($script:tracked['usuario'])"
        Assert-Status $usuarioDelete @(204) 'DELETE usuario'
        Add-Pass('DELETE /usuario/{id} responde 204 con borrado logico')

        $usuarioList = Get-JsonResponse -Method 'GET' -Url "$base/usuario"
        if ([int]$usuarioList.StatusCode -eq 200 -and (($usuarioList.Content | ConvertFrom-Json | Where-Object { $_.id_Usuario -eq $script:tracked['usuario'] }).Count -gt 0)) {
            Add-Fail('GET /usuario sigue listando un usuario marcado como eliminado')
        }
        else {
            Add-Pass('GET /usuario oculta usuarios eliminados logicamente')
        }

        $trabajadorDelete = Get-JsonResponse -Method 'DELETE' -Url "$base/trabajador/$($script:tracked['trabajador'])"
        Assert-Status $trabajadorDelete @(204) 'DELETE trabajador'
        Add-Pass('DELETE /trabajador/{id} responde 204 con borrado logico')

        $trabajadorList = Get-JsonResponse -Method 'GET' -Url "$base/trabajador"
        if ([int]$trabajadorList.StatusCode -eq 200 -and (($trabajadorList.Content | ConvertFrom-Json | Where-Object { $_.id_Trabajador -eq $script:tracked['trabajador'] }).Count -gt 0)) {
            Add-Fail('GET /trabajador sigue listando un trabajador marcado como eliminado')
        }
        else {
            Add-Pass('GET /trabajador oculta trabajadores eliminados logicamente')
        }

        try {
            $puestoDelete = Get-JsonResponse -Method 'DELETE' -Url "$base/puesto/$($script:tracked['puestoDependencia'])"
            Assert-Status $puestoDelete @(204) 'DELETE puesto con dependencia'
            Add-Pass('DELETE /puesto/{id} borro un puesto referenciado')
            $script:tracked['puestoDependencia'] = $null
        }
        catch {
            Add-Fail('DELETE /puesto/{id} falla si existen trabajadores historicos asociados; hoy responde 500 en lugar de una validacion controlada')
        }
    }
}
finally {
    try {
        Cleanup-TestData
    }
    catch {
        Add-Fail("Limpieza SQL directa fallo -> $($_.Exception.Message)")
    }

    if ($failures.Count -eq 0) {
        Write-Output 'CRUD_CHECK_OK'
    }
    else {
        Write-Output 'CRUD_CHECK_PARTIAL'
    }

    $results | ForEach-Object { Write-Output $_ }
    $failures | ForEach-Object { Write-Output $_ }
}
