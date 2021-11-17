(function ($) {
    var _bursaryService = abp.services.app.bursary,
        l = abp.localization.getSource('LXP2CYD'),
        _$modal = $('#BursaryCreateModal'),
        _$form = _$modal.find('form'),
        _$table = $('#BursariesTable');

    var _$bursariesTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        listAction: {
            ajaxFunction: _bursaryService.getAll,
            inputFilter: function () {
                return $('#BursariesSearchForm').serializeFormToObject(true);
            }
        },
        buttons: [
            {
                name: 'refresh',
                text: '<i class="fas fa-redo-alt"></i>',
                action: () => _$usersTable.draw(false)
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
                data: 'companyName',
                sortable: false
            },
            {
                targets: 3,
                data: 'link',
                sortable: false
            },
            {
                targets: 4,
                data: 'documentUrl',
                sortable: false
            },
            {
                targets: 5,
                data: 'isActive',
                sortable: false,
                render: data => `<input type="checkbox" disabled ${data ? 'checked' : ''}>`
            },
            {
                targets: 6,
                data: null,
                sortable: false,
                autoWidth: false,
                defaultContent: '',
                render: (data, type, row, meta) => {
                    return [
                        `   <button type="button" class="btn btn-sm bg-secondary edit-user" data-user-id="${row.id}" data-toggle="modal" data-target="#UserEditModal">`,
                        `       <i class="fas fa-pencil-alt"></i> ${l('Edit')}`,
                        '   </button>',
                        `   <button type="button" class="btn btn-sm bg-danger delete-user" data-user-id="${row.id}" data-user-name="${row.name}">`,
                        `       <i class="fas fa-trash"></i> ${l('Delete')}`,
                        '   </button>'
                    ].join('');
                }
            }
        ]
    });

    _$form.validate({
        rules: {
            Password: "required",
            ConfirmPassword: {
                equalTo: "#Password"
            }
        }
    });

    _$form.find('.save-button').on('click', (e) => {
        e.preventDefault();

        if (!_$form.valid()) {
            return;
        }

        var busrary = _$form.serializeFormToObject();
        //user.roleNames = [];
        //var _$roleCheckboxes = _$form[0].querySelectorAll("input[name='role']:checked");
        //if (_$roleCheckboxes) {
        //    for (var roleIndex = 0; roleIndex < _$roleCheckboxes.length; roleIndex++) {
        //        var _$roleCheckbox = $(_$roleCheckboxes[roleIndex]);
        //        user.roleNames.push(_$roleCheckbox.val());
        //    }
        //}

        abp.ui.setBusy(_$modal);
        _bursaryService.create(busrary).done(function () {
            _$modal.modal('hide');
            _$form[0].reset();
            abp.notify.info(l('SavedSuccessfully'));
            _$bursariesTable.ajax.reload();
        }).always(function () {
            abp.ui.clearBusy(_$modal);
        });
    });

    $(document).on('click', '.delete-user', function () {
        var bursaryId = $(this).attr("data-bursary-id");
        var bursaryName = $(this).attr('data-bursary-name');

        deleteUser(bursaryId, bursaryName);
    });

    function deleteBursary(userId, userName) {
        abp.message.confirm(
            abp.utils.formatString(
                l('AreYouSureWantToDelete'),
                userName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    _bursaryService.delete({
                        id: userId
                    }).done(() => {
                        abp.notify.info(l('SuccessfullyDeleted'));
                        _$bursariesTable.ajax.reload();
                    });
                }
            }
        );
    }

    $(document).on('click', '.edit-bursary', function (e) {
        var bursaryId = $(this).attr("data-bursary-id");

        e.preventDefault();
        abp.ajax({
            url: abp.appPath + 'Busraries/EditModal?bursary=' + bursaryId,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#BursaryEditModal div.modal-content').html(content);
            },
            error: function (e) {
            }
        });
    });

    $(document).on('click', 'a[data-target="#BursaryCreateModal"]', (e) => {
        $('.nav-tabs a[href="#create-bursary-details"]').tab('show')
    });

    abp.event.on('bursary.edited', (data) => {
        _$usersTable.ajax.reload();
    });

    _$modal.on('shown.bs.modal', () => {
        _$modal.find('input:not([type=hidden]):first').focus();
    }).on('hidden.bs.modal', () => {
        _$form.clearForm();
    });

    $('.btn-search').on('click', (e) => {
        _$usersTable.ajax.reload();
    });

    $('.txt-search').on('keypress', (e) => {
        if (e.which == 13) {
            _$usersTable.ajax.reload();
            return false;
        }
    });
})(jQuery);
