var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Marcas/GetAllMarcas",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            {
                "data": "nome", "width": "50%",
                "render": function (data) {
                    return `
                    <div>
                        <div class='text-black' style="color:black;>">
                            ${data}
                        </div>
                    </div>
                    `;
                }
            },
            //{
            //    "data": "state", "width": "20%",
            //    "render": function (data) {
            //        return `
            //        <div>
            //            <div class='text-black' style="color:black;>">
            //                ${data}
            //            </div>
            //        </div>
            //        `;
            //    }
            //},
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/marcas/Upsert/${data}" class='btn btn-success text-black'
                                    style='cursor:pointer;'> <i class='far fa-edit'></i></a>
                                    &nbsp;
                                <a onclick=Delete("/marcas/Delete/${data}") class='btn btn-danger text-black'
                                    style='cursor:pointer;'> <i class='far fa-trash-alt'></i></a>
                                </div>
                            `;
                }, "width": "30%"
            }
        ],
        "language": {
            "lengthMenu": "Mostrando _MENU_ entradas",
            "emptyTable": "Sem Dados encotrados.",
            "zeroRecords": "Sem Dados encontrados",
            "info": "Mostrando Pagina _PAGE_ de _PAGES_",
            "search": "Procurar",
            "paginate": {
                "previous": "Pagina Anterior",
                "next": "Proxima Pagina",
                "first": "Primeira Pagina"
            }
        },
        "width": "100%"
    });
}


function Delete(url) {
    swal({
        title: "Tens certeza que deseja deletar?",
        text: "Não sera capaz de restaurar!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmbuttonText: "Sim, deletar!",
        closeOnconfirm: true
    }, function () {
        $.ajax({
            type: 'Delete',
            url: url,
            success: function (data) {
                if (data.success) {
                    toastr.success(data.message);
                    dataTable.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }
            }
        });
    });
}