function showToastr(type, message) {
	if (type === 'success') {
		// Display a success toast, with a title
		toastr.success(message, 'Operación Exitosa');
	}
	if (type === 'error') {
		// Display an error toast, with a title
		toastr.error(message, 'Operación Fallida');
	}
}

function showSwal(type, message, headingTitle){
	if (type === 'success') {
		Swal.fire({
			title: headingTitle,
			text: message,
			icon: 'success'
		});
	}
	if (type === 'error') {
		Swal.fire({
			title: headingTitle,
			text: message,
			icon: 'error'
		});
	}
}

function activateLoading(){
	Swal.fire({
		text: 'Espere porfavor',
		allowOutsideClick: false,
		icon: 'info'
	});

Swal.showLoading();
}

function closeLoading(){
	Swal.close();
}

function swalConfirmation(message){
	return Swal.fire({
		title: 'Información',
		text: message,
		icon: 'warning',
		showCancelButton: true
	}).then((result) => {
		if (result.isConfirmed) {
			return true;
		} else {
			return false;
		}
	});
}