declare var window: any;

export class EXMConfig {

  public Init(): void {
    let t = sessionStorage.getItem('__EXM_CONFIG__');
    let e = document.getElementsByTagName('html')[0];
    let i = {
      theme: 'light',
      nav: 'vertical',
      layout: {
        mode: 'fluid',
        position: 'fixed'
      },
      topbar: {
        color: 'light'
      },
      sidenav: {
        color: 'dark',
        size: 'default',
        user: false
      }
    };

    let html = document.getElementsByTagName('html')[0];
    let config = Object.assign(JSON.parse(JSON.stringify(i)), {});
    let o = html.getAttribute('data-theme');

    config.theme = null !== o ? o : i.theme;
    o = html.getAttribute('data-layout');

    config.nav = null !== o ? 'topnav' === o ? 'horizontal' : 'vertical' : i.nav;
    o = html.getAttribute('data-layout-mode');

    config.layout.mode = null !== o ? o : i.layout.mode;
    o = html.getAttribute('data-layout-position');

    config.layout.position = null !== o ? o : i.layout.position;
    o = html.getAttribute('data-topbar-color');

    config.sidenav.color = null !== o ? o : i.sidenav.color;
    o = html.getAttribute('data-sidenav-size');

    config.sidenav.size = null !== o ? o : i.sidenav.size;
    o = html.getAttribute('data-sidenav-user');

    config.sidenav.user = null !== o || i.sidenav.user;
    window.defaultConfig = JSON.parse(JSON.stringify(config));

    if (t !== null) {
      config = JSON.parse(t);
    }

    window.config = config;

    config.nav = 'topnav' === e.getAttribute('data-layout') ? 'horizontal' : 'vertical';

    e.setAttribute('data-theme', config.theme);
    e.setAttribute('data-layout-mode', config.layout.mode);
    e.setAttribute('data-topbar-color', config.topbar.color);

    if (config.nav == 'vertical') {
      e.setAttribute('data-sidenav-size', config.sidenav.size);
      e.setAttribute('data-sidenav-color', config.sidenav.color);
      e.setAttribute('data-layout-position', config.layout.position);

      if (config.sidenav.user && 'true' === config.sidenav.user.toString()) {
        e.setAttribute('data-sidenav-user', 'true');
      } else {
        e.removeAttribute('data-sidenav-user');
      }
    }
  }

}
