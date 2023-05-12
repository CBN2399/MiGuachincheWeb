function mostrar(valor) {
    document.getElementById('valor').value = valor;
    document.getElementById('popup').style.display = 'block';
}

function cerrarFormulario() {
    document.getElementById('popup').style.display = 'none';
}