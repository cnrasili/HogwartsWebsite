// Shared scripts

// Active navigation state
(function () {
  const path = window.location.pathname.split('/').pop() || 'index.html';
  const navItems = document.querySelectorAll('.main-nav .nav-item');

  navItems.forEach(function (item) {
    const topLink = item.querySelector(':scope > .nav-link');
    const dropdownItems = item.querySelectorAll('.dropdown-item');

    if (topLink && topLink.getAttribute('href') === path) {
      topLink.classList.add('active');
      return;
    }

    dropdownItems.forEach(function (dropItem) {
      const dropPath = (dropItem.getAttribute('href') || '').split('#')[0];
      if (dropPath === path && topLink) {
        topLink.classList.add('active');
      }
    });
  });
}());
