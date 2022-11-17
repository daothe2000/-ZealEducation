
$(document).on('click', '.seat-edit', function () {
    var id = Number($(this).data('id'));

    GetDataById(id);
});

function GetDataById(_id) {

    $.ajax({
        url: '/Rooms/GetById',
        type: 'GET',
        data: { id: _id },
        dataType: 'json',
        success: function (response) {
            $('#room-id').val(response.RoomID);
            $('#room-name').val(response.RoomName);
            $('#ckStatus').prop('checked', response.status);

        }
    });
}

$(document).on('click', '#btn-add-rom', function () {
    if (Validate()) {
        var _seatId = $('#room-id').val();
        var _seatName = $('#room-name').val();
        var status = $('#ckStatus').prop('checked');

        var model = {
            RoomID: _seatId,
            RoomName: _seatName,
            status: status
        };

        AddRooms(model);
    }
});

function AddRooms(model) {
   
    $.ajax({
        url: '/Rooms/Create',
        type: 'POST',
        data: model,
        success: function (res) {
            if (res.success && !res.edit) {
                toastr.success('Create successful!', 'Success!', {
                    closeButton: true,
                    tapToDismiss: false
                    
                });
               
            } else if (!res.success && !res.edit) {
                toastr.error('Create failed!', 'Error!', {
                    closeButton: true,
                    tapToDismiss: false
                });
            }

            if (res.success && res.edit) {
                toastr.success('Update successful!', 'Success!', {
                    closeButton: true,
                    tapToDismiss: false
                });
            } else if (!res.success && res.edit) {
                toastr.error('Update failed!', 'Error!', {
                    closeButton: true,
                    tapToDismiss: false
                });
            }
            $('#room-id').val(0);
        }
    });
    window.location.reload(true);

}

$(document).on('click', '.seat-delete', function () {
    var id = Number($(this).data('id'));

    DeleteSeat(id);
});

function DeleteSeat(id) {
    $.ajax({
        url: '/Rooms/Delete',
        type: 'POST',
        data: { id: id },
        
        success: function (res) {

            if (res.success) {
                
              
               
                $('#room-id').val(0);
                $('#room-name').val('');
                $('#ckStatus').prop('checked');

                toastr.success('Delete successful!', 'Success!', {
                    

                    closeButton: true,
                    tapToDismiss: false
                });
            } else {
                toastr.error('Delete failed!', 'Error!', {
                    closeButton: true,
                    tapToDismiss: false
                });
            }
      
            
        }
    });
    window.location.reload(true);
}
//function DeleteSeat(id) {
//    swal({
//        title: "Are You Sure?",
//        text: "Do You Want To Delete This Information?",
//        type: "warning",
//        showCancelButton: true,
//        confirmButtonClass: "btn btn-default swal2-btn-default",
//        cancelButtonClass: "btn btn-default swal2-btn-default",
//        confirmButtonText: "Yes, Continue",
//        cancelButtonText: "Cancel",
//        buttonsStyling: false,
//        footer: "*Note : This data will be permanently deleted"
//    }).then((result) => {
//        if (result.value) {
//            $.ajax({
//                url: '/Rooms/Delete',
//                type: 'POST',
//                data: { id: id },
//                success: function (res) {
//                    if (res.success) {
//                        loadData("", null, 10);

//                        $('#room-id').val(0);
//                        $('#room-name').val('');
//                        $('#ckStatus').prop('checked');

//                        toastr.success('Delete successful!', 'Success!', {
//                            closeButton: true,
//                            tapToDismiss: false
//                        });
//                    } else {
//                        toastr.error('Delete failed!', 'Error!', {
//                            closeButton: true,
//                            tapToDismiss: false
//                        });
//                    }
//                    loadData($('#pro_search').val(), null, 10);
//                }
//            });
//        }
//    });
//}
function Validate() {
    var check = false;
    var _seatName = $('#room-name').val();

    if (_seatName === '') {
        $('#room-name').parent().next().html('Name cannot be empty!');
        check = false;
    } else {
        $('#room-name').parent().next().html('');
        check = true;
    }



    return check;
}