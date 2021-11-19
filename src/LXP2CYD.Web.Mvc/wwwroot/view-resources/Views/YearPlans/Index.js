(function ($) {
    var _yearPlanService = abp.services.app.yearPlan,
        l = abp.localization.getSource('LXP2CYD'),
        _$modal = $('#YearPlanCreateModal'),
        _$form = _$modal.find('form'),
        _$table = $('#YearPlansTable');

    var _$yearPlansTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        listAction: {
            ajaxFunction: _yearPlanService.getAll,
            inputFilter: function () {
                return $('#YearPlanSearchForm').serializeFormToObject(true);
            }
        },
        buttons: [
            {
                name: 'refresh',
                text: '<i class="fas fa-redo-alt"></i>',
                action: () => _$yearPlansTable.draw(false)
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
                data: 'description',
                sortable: false
            },
            {
                targets: 2,
                data: 'year',
                sortable: false
            },
            {
                targets: 3,
                data: 'status',
                sortable: false
            },
            {
                targets:4,
                data: 'isActive',
                sortable: false,
                render: data => `<input type="checkbox" disabled ${data ? 'checked' : ''}>`
            },
            {
                targets: 5,
                data: null,
                sortable: false,
                autoWidth: false,
                defaultContent: '',
                render: (data, type, row, meta) => {
                    return [
                        `   <button type="button" class="btn btn-sm bg-secondary edit-year-plan" data-year-plan-id="${row.id}" data-toggle="modal" data-target="#UserEditModal">`,
                        `       <i class="fas fa-pencil-alt"></i> ${l('Edit')}`,
                        '   </button>',
                        `   <button type="button" class="btn btn-sm bg-danger delete-year-plan" data-year-plan-id="${row.id}" data-year-plan-name="${row.year}">`,
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

        var user = _$form.serializeFormToObject();

        abp.ui.setBusy(_$modal);
        _yearPlanService.create(user).done(function () {
            _$modal.modal('hide');
            _$form[0].reset();
            abp.notify.info(l('SavedSuccessfully'));
            _$yearPlansTable.ajax.reload();
        }).always(function () {
            abp.ui.clearBusy(_$modal);
        });
    });

    $(document).on('click', '.delete-year-plan', function () {
        var yearPlanrId = $(this).attr("data-year-plan-id");
        var yearPlanName = $(this).attr('data-year-plan-name');

        deleteYearPlan(yearPlanrId, yearPlanName);
    });

    function deleteUser(yearPlanrId, yearPlanName) {
        abp.message.confirm(
            abp.utils.formatString(
                l('AreYouSureWantToDelete'),
                userName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    _yearPlanService.delete({
                        id: yearPlanrId
                    }).done(() => {
                        abp.notify.info(l('SuccessfullyDeleted'));
                        _$yearPlansTable.ajax.reload();
                    });
                }
            }
        );
    }

    $(document).on('click', '.edit-year-plan', function (e) {
        var userId = $(this).attr("data-year-plan-id");

        e.preventDefault();
        abp.ajax({
            url: abp.appPath + 'YearPlans/EditModal?userId=' + userId,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#YearPlanEditModal div.modal-content').html(content);
            },
            error: function (e) {
            }
        });
    });

    $(document).on('click', 'a[data-target="#UserCreateModal"]', (e) => {
        $('.nav-tabs a[href="#create-year-plan-details"]').tab('show')
    });

    abp.event.on('user.edited', (data) => {
        _$yearPlansTable.ajax.reload();
    });

    _$modal.on('shown.bs.modal', () => {
        _$modal.find('input:not([type=hidden]):first').focus();
    }).on('hidden.bs.modal', () => {
        _$form.clearForm();
    });

    $('.btn-search').on('click', (e) => {
        _$yearPlansTable.ajax.reload();
    });

    $('.txt-search').on('keypress', (e) => {
        if (e.which == 13) {
            _$yearPlansTable.ajax.reload();
            return false;
        }
    });
})(jQuery);
