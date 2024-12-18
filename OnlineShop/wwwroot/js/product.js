﻿$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/product/getAll' },
        "columns": [
            { data: 'title', width: "25%" },
            { data: 'isbn', width: "15%" },
            { data: 'price', width: "10%" },
            { data: 'author', width: "15%" },
            { data: 'category.name', width: "10%" },
            {
                data: 'id',
                render: function (data) {
                    return `<div class="w-75 btn-group" role="group">
                        <a href="/admin/product/upsert?id=${data}" class="btn btn-primary mx-2">
                            <i class="bi bi-pencil-square">Edit</i>
                        </a>
                         <a href="/admin/product/delete/${data}" class="btn btn-danger mx-2">
                            <i class="bi bi-trash-fill">Delete</i>
                        </a>
                    </div>`
                },
                with: "25%"
            }
        ]
    });
}


