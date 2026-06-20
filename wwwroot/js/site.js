document.addEventListener('DOMContentLoaded', function () {
    // Auto-dismiss alerts
    document.querySelectorAll('.alert-dismissible').forEach(function (alert) {
        setTimeout(function () {
            var closeButton = alert.querySelector('.btn-close');
            if (closeButton) {
                closeButton.click();
            }
        }, 8000);
    });

    // Delete confirmations
    document.querySelectorAll('[data-confirm-delete]').forEach(function (button) {
        button.addEventListener('click', function (event) {
            var message = button.getAttribute('data-confirm-delete') || 'Are you sure?';
            if (!window.confirm(message)) {
                event.preventDefault();
            }
        });
    });

    document.querySelectorAll('form[data-confirm]').forEach(function (form) {
        form.addEventListener('submit', function (event) {
            var message = form.getAttribute('data-confirm') || 'Are you sure?';
            if (!window.confirm(message)) {
                event.preventDefault();
            }
        });
    });

    // Mobile sidebar
    var sidebar = document.getElementById('tmSidebar');
    var backdrop = document.getElementById('sidebarBackdrop');
    var toggle = document.getElementById('sidebarToggle');

    function closeSidebar() {
        if (!sidebar) return;
        sidebar.classList.remove('is-open');
        if (backdrop) backdrop.hidden = true;
        if (toggle) toggle.setAttribute('aria-expanded', 'false');
    }

    function openSidebar() {
        if (!sidebar) return;
        sidebar.classList.add('is-open');
        if (backdrop) backdrop.hidden = false;
        if (toggle) toggle.setAttribute('aria-expanded', 'true');
    }

    if (toggle) {
        toggle.addEventListener('click', function () {
            if (sidebar.classList.contains('is-open')) {
                closeSidebar();
            } else {
                openSidebar();
            }
        });
    }

    if (backdrop) {
        backdrop.addEventListener('click', closeSidebar);
    }

    // Bootstrap tooltips for action buttons
    document.querySelectorAll('[data-bs-toggle="tooltip"]').forEach(function (el) {
        new bootstrap.Tooltip(el);
    });
});
