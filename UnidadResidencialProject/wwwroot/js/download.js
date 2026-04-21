window.descargarArchivo = function (nombre, base64) {
    const link = document.createElement('a');
    link.href = 'data:application/octet-stream;base64,' + base64;
    link.download = nombre;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
};