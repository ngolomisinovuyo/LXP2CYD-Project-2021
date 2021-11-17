(function ($) {
    var _programmeService = abp.services.app.programme,
        l = abp.localization.getSource('LXP2CYD'),
        _$modal = $('#ProgrammeCreateModal'),
        _$form = _$modal.find('form'),
        _$table = $('#ProgrammesTable');

    var _$programmesTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        listAction: {
            ajaxFunction: _userService.getAll,
            inputFilter: function () {
                return $('#ProgrammesSearchForm').serializeFormToObject(true);
            }
        },
        buttons: [
            {
                name: 'refresh',
                text: '<i class="fas fa-redo-alt"></i>',
                action: () => _$programmesTable.draw(false)
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
                data: 'fullName',
                sortable: false
            },
            {
                targets: 2,
                data: 'emailAddress',
                sortable: false
            },
            {
                targets: 3,
                data: 'phoneNumber',
                sortable: false
            },
            {
                targets: 4,
                data: 'roleNames',
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

        var programme = _$form.serializeFormToObject();
        abp.ui.setBusy(_$modal);
        _programmeService.create(programme).done(function () {
            _$modal.modal('hide');
            _$form[0].reset();
            abp.notify.info(l('SavedSuccessfully'));
            _$programmesTable.ajax.reload();
        }).always(function () {
            abp.ui.clearBusy(_$modal);
        });
    });

    $(document).on('click', '.programme-user', function () {
        var programmeId = $(this).attr("data-programme-id");
        var programmeName = $(this).attr('data-programme-name');

        deleteUser(programmeId, programmeName);
    });

    function deleteUser(programmeId, programmeName) {
        abp.message.confirm(
            abp.utils.formatString(
                l('AreYouSureWantToDelete'),
                programmeName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    _programmeService.delete({
                        id: uprogrammeId
                    }).done(() => {
                        abp.notify.info(l('SuccessfullyDeleted'));
                        _$usersTable.ajax.reload();
                    });
                }
            }
        );
    }

    $(document).on('click', '.edit-programme', function (e) {
        var programmeId = $(this).attr("data-programme-id");

        e.preventDefault();
        abp.ajax({
            url: abp.appPath + 'Programmes/EditModal?programmeId=' + programmeId,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#ProgrammeEditModal div.modal-content').html(content);
            },
            error: function (e) {
            }
        });
    });

    $(document).on('click', 'a[data-target="#ProgammeCreateModal"]', (e) => {
        $('.nav-tabs a[href="#create-programme-details"]').tab('show')
    });

    abp.event.on('user.edited', (data) => {
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
