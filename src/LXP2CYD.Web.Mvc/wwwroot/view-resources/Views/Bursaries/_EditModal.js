(function ($) {
    var _bursaryService = abp.services.app.bursary,
        l = abp.localization.getSource('LXP2CYD'),
        _$modal = $('#UserEditModal'),
        _$form = _$modal.find('form');

    function save() {
        if (!_$form.valid()) {
            return;
        }

        var bursary = _$form.serializeFormToObject();
        //user.roleNames = [];
        //var _$roleCheckboxes = _$form[0].querySelectorAll("input[name='role']:checked");
        //if (_$roleCheckboxes) {
        //    for (var roleIndex = 0; roleIndex < _$roleCheckboxes.length; roleIndex++) {
        //        var _$roleCheckbox = $(_$roleCheckboxes[roleIndex]);
        //        user.roleNames.push(_$roleCheckbox.val());
        //    }
        //}

        abp.ui.setBusy(_$form);
        _bursaryService.update(bursary).done(function () {
            _$modal.modal('hide');
            abp.notify.info(l('SavedSuccessfully'));
            abp.event.trigger('bursary.edited', bursary);
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
