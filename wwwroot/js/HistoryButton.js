const DOM = {
    boton: document.getElementById("ManagerButton"),
    container: document.getElementById("historial")
};
DOM.boton.addEventListener("click", () => {
    DOM.container.classList.toggle("oculto");
})