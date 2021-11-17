(function ($) {
    var _appointmentService = abp.services.app.appointment,
        l = abp.localization.getSource('LXP2CYD'),
        _$modal = $('#AppointmentCreateModal'),
        _$form = _$modal.find('form'),
        _$table = $('#AppointmentsTable');

    var _$appointmentsTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        listAction: {
            ajaxFunction: _appointmentService.getAll,
            inputFilter: function () {
                return $('#AppointmentsSearchForm').serializeFormToObject(true);
            }
        },
        buttons: [
            {
                name: 'refresh',
                text: '<i class="fas fa-redo-alt"></i>',
                action: () => _$appointmentsTable.draw(false)
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
                data: 'userName',
                sortable: false
            },
            {
                targets: 2,
                data: 'fullName',
                sortable: false
            },
            {
                targets: 3,
                data: 'emailAddress',
                sortable: false
            },
            {
                targets: 4,
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
                        `   <button type="button" class="btn btn-sm bg-secondary edit-appointment" data-appointment-id="${row.id}" data-toggle="modal" data-target="#UserEditModal">`,
                        `       <i class="fas fa-pencil-alt"></i> ${l('Edit')}`,
                        '   </button>',
                        `   <button type="button" class="btn btn-sm bg-danger delete-appointment" data-appointment-id="${row.id}" data-appointment-name="${row.name}">`,
                        `       <i class="fas fa-trash"></i> ${l('Delete')}`,
                        '   </button>'
                    ].join('');
                }
            }
        ]
    });

    getAppointments();
    _$form.validate({
        rules: {
            Password: "required",
            ConfirmPassword: {
                equalTo: "#Password"
            }
        }
    });
    _$form.find('#startDatePicker').datetimepicker({
        "allowInputToggle": true,
        "showClose": true,
        "showClear": true,
        "showTodayButton": true,
        "format": "DD-MM-YYYY",

    });
    _$form.find('#endDatePicker').datetimepicker({
        "allowInputToggle": true,
        "showClose": true,
        "showClear": true,
        "showTodayButton": true,
        "format": "DD-MM-YYYY",
    });
    _$form.find('#startTimePicker').datetimepicker({
        "allowInputToggle": true,
        "showClose": true,
        "showClear": true,
        "showTodayButton": true,
        "format": "HH:mm:ss",
    });
    _$form.find('#endTimePicker').datetimepicker({
        "allowInputToggle": true,
        "showClose": true,
        "showClear": true,
        "showTodayButton": true,
        "format": "HH:mm:ss",
    });
   _$form.find('.selectpicker').selectpicker();

    _$form.find('#IsVirtual').change(function () {
        if (this.checked) {
            _$form.find('#link_rapper').show();
            _$form.find('#location_wrapper').hide();
        } else {
            _$form.find('#link_rapper').hide();
            _$form.find('#location_wrapper').show();
        }
    });
    _$form.find('.save-button').on('click', (e) => {
        e.preventDefault();

        if (!_$form.valid()) {
            return;
        }

        const appointment = _$form.serializeFormToObject();
        appointment.StartTime = new Date(appointment.StartDate.split('-')
            .reverse().join().replace(/,/g, '-') + ' ' + appointment.StartTime);
        appointment.EndTime = new Date(appointment.EndDate.split('-')
            .reverse().join().replace(/,/g, '-') + ' ' + appointment.EndTime)

        delete appointment.EndDate;
        delete appointment.StartDate;
        //console.log(_$form.find('#select-attendees').val());
        appointment.CreateAppointmentAttendeeDtos = _$form.find('#select-attendees').val().map(x => JSON.parse(x));
        abp.ui.setBusy(_$modal);
        _appointmentService.create(appointment).done(function () {
            _$modal.modal('hide');
            _$form[0].reset();
            abp.notify.info(l('SavedSuccessfully'));
            _$usersTable.ajax.reload();
        }).always(function () {
            abp.ui.clearBusy(_$modal);
        });
    });

    $(document).on('click', '.delete-user', function () {
        var appointmentId = $(this).attr("data-appointment-id");
        var appointmentName = $(this).attr('data-appointment-name');

        deleteUser(appointmentId, appointmentName);
    });

    function deleteAppointment(appointmentId, appointmenTitle) {
        abp.message.confirm(
            abp.utils.formatString(
                l('AreYouSureWantToDelete'),
                userName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    _appointmentService.delete({
                        id: appointmentId
                    }).done(() => {
                        abp.notify.info(l('SuccessfullyDeleted'));
                        _$usersTable.ajax.reload();
                    });
                }
            }
        );
    }

    $(document).on('click', '.edit-appointment', function (e) {
        var appointmentId = $(this).attr("data-appointment-id");

        e.preventDefault();
        abp.ajax({
            url: abp.appPath + 'Appointment/EditModal?appointmentId=' + appointmentId,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#AppointmentEditModal div.modal-content').html(content);
            },
            error: function (e) {
            }
        });
    });

    abp.event.on('appointment.edited', (data) => {
        _$usersTable.ajax.reload();
    });
    $(document).on('click', 'a[data-target="#AppointmentCreateModal"]', (e) => {
        $('.nav-tabs a[href="#create-appointment-details"]').tab('show')
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
    function getAppointments() {
        const events = [];
        $.ajax({
            type: "GET",
            url: "/appointments/GetAppointments",
            success: function (data) {
                const results = data['result'];
                if (results.length > 0) {
                    $.each(results, function (i, v) {
                        if (v) {
                            events.push({
                                title: v.title,
                                description: v.Description,
                                start: new Date(v.startTime),
                                end: v.endTime != null ? moment(v.endTime) : null,
                                color: v.ThemeColor,
                                id: v.id,
                                allDay: v.IsFullDay
                            });
                        }
                       
                    })
                }
               
                GenerateCalender(events);
            },
            error: function (error) {
                alert('failed');
            }
        })
    }
    
    function GenerateCalender(events) {
        $('#calender').fullCalendar('destroy');
        $('#calender').fullCalendar({
            contentHeight: 400,
            defaultDate: new Date(),
            timeFormat: 'h(:mm)a',
            header: {
                left: 'prev,next today',
                center: 'title',
                right: 'month,basicWeek,basicDay,agenda'
            },
            eventLimit: true,
            eventColor: '#378006',
            events: events,
            eventClick: function (calEvent, jsEvent, view) {
               // $('#AppointmemtEditModal #eventTitle').value(calEvent.title);
               // var $description = $('<div/>');
               // $description.append($('<p/>').html('<b>Start:</b>' + calEvent.start.format("DD-MMM-YYYY HH:mm a")));
               // if (calEvent.end != null) {
               //     $description.append($('<p/>').html('<b>End:</b>' + calEvent.end.format("DD-MMM-YYYY HH:mm a")));
               // }
               // $description.append($('<p/>').html('<b>Description:</b>' + calEvent.description));
               //// $('#myModal #pDetails').empty().html($description);
                $('#AppointmemtEditModal').modal();
            }
        })
    }
})(jQuery);
