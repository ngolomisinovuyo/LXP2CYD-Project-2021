(function ($) {
    var _appointmentService = abp.services.app.appointment,
        l = abp.localization.getSource('LXP2CYD'),
        _$modal = $('#AppointmentEditModal'),
        _$form = _$modal.find('form');
    //_$form.find('.selectpicker').selectpicker();
    function save() {
        if (!_$form.valid()) {
            return;
        }

        var appointmemt = _$form.serializeFormToObject();
        appointment.StartTime = new Date(appointment.StartDate.split('-')
            .reverse().join().replace(/,/g, '-') + ' ' + appointment.StartTime);
        appointment.EndTime = new Date(appointment.EndDate.split('-')
            .reverse().join().replace(/,/g, '-') + ' ' + appointment.EndTime)

        delete appointment.EndDate;
        delete appointment.StartDate;
        appointment.CreateAppointmentAttendeeDtos = _$form.find('#select-attendees').val().map(x => JSON.parse(x));
        abp.ui.setBusy(_$form);
        _appointmentService.update(appointment).done(function () {
            _$modal.modal('hide');
            abp.notify.info(l('SavedSuccessfully'));
            abp.event.trigger('appointment.edited', user);
        }).always(function () {
            abp.ui.clearBusy(_$form);
        });
    }

    _$form.closest('div.modal-content').find(".save-button").click(function (e) {
        e.preventDefault();
        save();
    });

    _$form.find('input').on('keypress', function (e) {
        if (e.which === 13) {
            e.preventDefault();
            save();
        }
    });

    _$modal.on('shown.bs.modal', function () {
        _$form.find('input[type=text]:first').focus();
    });
})(jQuery);
