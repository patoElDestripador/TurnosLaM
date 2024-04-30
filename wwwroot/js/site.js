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
    text: "¡Turno asignado. En breve un asesor te llamará!",
    icon: "success",
    timer: 5000
  });
}

function documentNotFound(Document) {
  const swalWithBootstrapButtons = Swal.mixin({
    customClass: {
      confirmButton: "btn btn-success ms-4",
      cancelButton: "btn btn-info"
    },
    buttonsStyling: false
  });

  swalWithBootstrapButtons.fire({
    title: `¿Deseas continuar de todas formas?`,
    text: `No econtramos el documento con numero ${Document}`,
    icon: "warning",
    showCancelButton: true,
    confirmButtonText: "Continuar",
    cancelButtonText: "Cambiar",
    reverseButtons: true
  }).then((result) => {
    if (result.isConfirmed) {
      window.location.href = "/Users/Services";
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

function notificationShift(message) {
  Swal.fire({
    position: "Center",
    icon: "info",
    title: `Turno : ${message}
    Modulo : 2`,
    showConfirmButton: false,
    timer: 5000
  });
}

function noPacients(params) {
  Swal.fire({
    position: "Center",
    icon: "info",
    title: `${params}`,
    showConfirmButton: false,
    timer: 2000
  });
}