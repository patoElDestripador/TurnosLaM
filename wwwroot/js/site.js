// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



function hola() {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
      }).then((result) => {
        if (result.isConfirmed) {
          Swal.fire({
            title: "Deleted!",
            text: "Your file has been deleted.",
            icon: "success"
          });
        }
      });

}

function shiftNotifier(ShiftNumber) {
  Swal.fire({
    title: `Turno ${ShiftNumber}`,
    text: "Este es tu turno, En breve un asesor te llamara!!",
    icon: "success"
  });
}

function documentNotFound(Document) {
  const swalWithBootstrapButtons = Swal.mixin({
    customClass: {
      confirmButton: "btn btn-success",
      cancelButton: "btn btn-info"
    },
    buttonsStyling: false
  });

  swalWithBootstrapButtons.fire({
    title:`¿Deseas continuar de todas formas?`,
    text: `No econtramos el documento con numero ${Document}`,
    icon: "warning",
    showCancelButton: true,
    confirmButtonText: "Continuar",
    cancelButtonText: "Cambiar",
    reverseButtons: true
  }).then((result) => {
    if (result.isConfirmed) {
        //generar el turno
      }
  });

}


function notificationToMissionController(message) {
  Swal.fire({
    position: "top-end",
    icon: "warning",
    title: `${message}`,
    showConfirmButton: false,
    timer: 1000
  }); 
}

//esta funcion solo sirve en una direccion 
//si requieren una respuesta...
//consiguren la de uds 
function sendRequestToController(Controller,method,methodType = "GET",params = "") {
  var xhr = new XMLHttpRequest();
  xhr.open(methodType, `/${Controller}/${method}/${params}`, true);
  xhr.onload = () => {
      if (xhr.status >= 200 && xhr.status < 300) {
          console.log(xhr.responseText);
      } else {
          console.error(xhr.statusText);
      }
  };
  xhr.send();
}


