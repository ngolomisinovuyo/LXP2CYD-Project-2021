(function ($) {
    var _subjectService = abp.services.app.subject,
        l = abp.localization.getSource('LXP2CYD'),
        _$modal = $('#UserCreateModal'),
        _$form = _$modal.find('form'),
        _$table = $('#UsersTable');

    var _$subjectsTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        listAction: {
            ajaxFunction: _subjectService.getAll,
            inputFilter: function () {
                return $('#SubjectsSearchForm').serializeFormToObject(true);
            }
        },
        buttons: [
            {
                name: 'refresh',
                text: '<i class="fas fa-redo-alt"></i>',
                action: () => _$subjectsTable.draw(false)
            }
        ],
        responsive: {
            details: {
                type: 'column'
            }
        },
        columnDefs: [
            {
                targets: 0,
                className: 'control',
                defaultContent: ''
            },
            {
                targets: 1,
                data: 'name',
                sortable: false
            },
            {
                targets: 2,
                data: 'description',
                sortable: false
            },
            {
                targets: 3,
                data: 'isActive',
                sortable: false,
                render: data => `<input type="checkbox" disabled ${data ? 'checked' : ''}>`
            },
            {
                targets: 4,
                data: null,
                sortable: false,
                autoWidth: false,
                defaultContent: '',
                render: (data, type, row, meta) => {
                    return [
                        `   <button type="button" class="btn btn-sm bg-secondary edit-subject" data-subject-id="${row.id}" data-toggle="modal" data-target="#SubjectEditModal">`,
                        `       <i class="fas fa-pencil-alt"></i> ${l('Edit')}`,
                        '   </button>',
                        `   <button type="button" class="btn btn-sm bg-danger delete-subject" data-subject-id="${row.id}" data-subject-name="${row.name}">`,
                        `       <i class="fas fa-trash"></i> ${l('Delete')}`,
                        '   </button>'
                    ].join('');
                }
            }
        ]
    });


    _$form.find('.save-button').on('click', (e) => {
        e.preventDefault();

        if (!_$form.valid()) {
            return;
        }
        var subject = _$form.serializeFormToObject();
        abp.ui.setBusy(_$modal);
        _subjectService.create(subject).done(function () {
            _$modal.modal('hide');
            _$form[0].reset();
            abp.notify.info(l('SavedSuccessfully'));
            _$subjectsTable.ajax.reload();
        }).always(function () {
            abp.ui.clearBusy(_$modal);
        });
    });

    $(document).on('click', '.delete-subject', function () {
        var id = $(this).attr("data-subject-id");
        var name = $(this).attr('data-subject-name');

        deleteUser(id, name);
    });

    function deleteUser(id, name) {
        abp.message.confirm(
            abp.utils.formatString(
                l('AreYouSureWantToDelete'),
                userName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    _subjectService.delete({
                        id: id
                    }).done(() => {
                        abp.notify.info(l('SuccessfullyDeleted'));
                        _$subjectsTable.ajax.reload();
                    });
                }
            }
        );
    }

    $(document).on('click', '.edit-subject', function (e) {
        var userId = $(this).attr("data-subject-id");

        e.preventDefault();
        abp.ajax({
            url: abp.appPath + 'Subjects/EditModal?userId=' + userId,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#SubjectEditModal div.modal-content').html(content);
            },
            error: function (e) {
            }
        });
    });

    $(document).on('click', 'a[data-target="#SubjectCreateModal"]', (e) => {
        $('.nav-tabs a[href="#create-subject-details"]').tab('show')
    });

    abp.event.on('subject.edited', (data) => {
        _$subjectsTable.ajax.reload();
    });

    _$modal.on('shown.bs.modal', () => {
        _$modal.find('input:not([type=hidden]):first').focus();
    }).on('hidden.bs.modal', () => {
        _$form.clearForm();
    });

    $('.btn-search').on('click', (e) => {
        _$subjectsTable.ajax.reload();
    });

    $('.txt-search').on('keypress', (e) => {
        if (e.which == 13) {
            _$subjectsTable.ajax.reload();
            return false;
        }
    });
})(jQuery);
