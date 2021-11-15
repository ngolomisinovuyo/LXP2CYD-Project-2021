(function ($) {
    var _regionService = abp.services.app.region,
        l = abp.localization.getSource('LXP2CYD'),
        _$modal = $('#RegionCreateModal'),
        _$form = _$modal.find('form'),
        _$table = $('#RegionsTable');

    var _$regionsTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        listAction: {
            ajaxFunction: _regionService.getAll,
            inputFilter: function () {
                return $('#RegionsSearchForm').serializeFormToObject(true);
            }
        },
        buttons: [
            {
                name: 'refresh',
                text: '<i class="fas fa-redo-alt"></i>',
                action: () => _$regionsTable.draw(false)
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
                defaultContent: '',
            },
            {
                targets: 1,
                data: 'name',
                sortable: false
            },
            {
                targets: 2,
                data: 'province.name',
                sortable: false
            },
            {
                targets: 3,
                data: 'noOfCenters',
                sortable: false,
            },
            {
                targets: 4,
                data: null,
                sortable: false,
                autoWidth: false,
                defaultContent: '',
                render: (data, type, row, meta) => {
                    return [
                        `   <button type="button" class="btn btn-sm bg-secondary edit-region" data-region-id="${row.id}" data-toggle="modal" data-target="#RegionEditModal">`,
                        `       <i class="fas fa-pencil-alt"></i> ${l('Edit')}`,
                        '   </button>',
                        `   <button type="button" class="btn btn-sm bg-danger delete-region" data-region-id="${row.id}" data-region-name="${row.name}">`,
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

        var region = _$form.serializeFormToObject();
        
        abp.ui.setBusy(_$modal);
        _regionService.create(region).done(function () {
            _$modal.modal('hide');
            _$form[0].reset();
            abp.notify.info(l('SavedSuccessfully'));
            _$regionsTable.ajax.reload();
        }).always(function () {
            abp.ui.clearBusy(_$modal);
        });
    });

    $(document).on('click', '.delete-region', function () {
        var regionId = $(this).attr("data-region-id");
        var regionName = $(this).attr('data-region-name');

        deleteRegion(regionId, regionName);
    });

    function deleteRegion(regionName, regionName) {
        abp.message.confirm(
            abp.utils.formatString(
                l('AreYouSureWantToDelete'),
                userName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    _regionsService.delete({
                        id: userId
                    }).done(() => {
                        abp.notify.info(l('SuccessfullyDeleted'));
                        _$usersTable.ajax.reload();
                    });
                }
            }
        );
    }

    $(document).on('click', '.edit-region', function (e) {
        var regionId = $(this).attr("data-region-id");

        e.preventDefault();
        abp.ajax({
            url: abp.appPath + 'Regions/EditModal?regionId=' + regionId,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#RegionsEditModal div.modal-content').html(content);
            },
            error: function (e) {
            }
        });
    });

    $(document).on('click', 'a[data-target="#RegionCreateModal"]', (e) => {
        $('.nav-tabs a[href="#region-details"]').tab('show')
    });

    abp.event.on('region.edited', (data) => {
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
