(function ($) {
  "use strict";
  var emailEnableSlug
    , $emailEnableModal
    ;

  $emailEnableModal = $('#enableEmailModal').modal({
    show: false
  });

  $('#sitesTable').on('click', '.enable-email-notification', function (e) {
    var $el = $(e.currentTarget);
    emailEnableSlug = $($el.parents('tr').get(0)).data('slug');
    $('#emailAddressError').hide();
    $emailEnableModal.modal('show');
  });

  $('#confirmEmailButton').click(function () {
    var email = $('#emailAddress').val(),
        $status = $('#emailStatus');
    if (!email || email.length === 0) {
      $('#emailAddressError').show();
      return;
    }
    $('#emailAddressError').hide();

    $status.html('Enabling...');
    $.ajax({
      type: 'PUT',
      url: '/Sites/' + emailEnableSlug + '/Notifications/Email',
      data: { email: email },
      success: function (data) {
        if (data && data.ok) {
          $emailEnableModal.modal('hide');
          $status.html('');
        } else {
          $status.html('<span class="label label-important">' + data.message || 'An error occurred.' + '</span>');
        }
      },
      error: function () {
        $status.html('<span class="label label-important">Unable to complete at this time.</span>');
      }
    });
  });
} (window.jQuery));