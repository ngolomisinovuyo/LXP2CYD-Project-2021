(function ($) {
    var _programmeService = abp.services.app.programme,
        l = abp.localization.getSource('LXP2CYD'),
        _$modal = $('#ProgrammeEditModal'),
        _$form = _$modal.find('form');
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
    _$form.find('#select-attendees').selectpicker();
    _$form.find('#IsVirtual').change(function () {
        if (this.checked) {
            _$form.find('#link_rapper').show();
            _$form.find('#location_wrapper').hide();
        } else {
            _$form.find('#link_rapper').hide();
            _$form.find('#location_wrapper').show();
        }
    });
    function save() {
        if (!_$form.valid()) {
            return;
        }

        var programme = _$form.serializeFormToObject();
        programme.StartDate = moment(programme.StartDate.split('-')
            .reverse().join().replace(/,/g, '-') + ' ' + programme.StartTime);
        programme.EndDate = moment(programme.EndDate.split('-')
            .reverse().join().replace(/,/g, '-') + ' ' + programme.EndTime)

        delete programme.EndTime;
        delete programme.StartTime;
        abp.ui.setBusy(_$form);
        _programmeService.update(programme).done(function () {
            _$modal.modal('hide');
            abp.notify.info(l('SavedSuccessfully'));
            abp.ui.clearBusy(_$form);
            abp.event.trigger('programme.edited', user);
        }).always(function () {
            abp.ui.clearBusy(_$form);
        });
    }
    // Fix later
    _$form.find(".save-button").click(function (e) {
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
