import React, { Component } from "react";
import "./NavMenu.css";

export class NavMenu extends Component {
  static displayName = NavMenu.name;

  constructor(props) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.state = {
      collapsed: true,
      isAuth: false,
    };
  }

  toggleNavbar() {
    this.setState({
      collapsed: !this.state.collapsed,
      isAuth: this.state.isAuth,
    });
  }
  componentDidMount() {
    this.isAuthenticated();
  }
  renderNavbar() {
    if (!this.state.isAuth) {
      return (
        <React.Fragment>
          <ul className="navbar-nav">
            <li className="nav-item">
              <a className="nav-link" href="/prywatnosc">
                Prywatność
              </a>
            </li>
          </ul>
          <ul className="navbar-nav ml-auto">
            <li className="nav-item">
              <a href="/konto" className="nav-link">
                Zaloguj/Zarejestruj
              </a>
            </li>
          </ul>
        </React.Fragment>
      );
    } else {
      return (
        <React.Fragment>
          <ul className="navbar-nav">
            <li className="nav-item">
              <a className="nav-link" href="/harmonogram">
                Harmonogram
              </a>
            </li>
            <li className="nav-item">
              <a className="nav-link" href="/trening">
                Treningi
              </a>
            </li>
            <li className="nav-item">
              <a className="nav-link" href="/cwiczenia">
                Ćwiczenia
              </a>
            </li>
          </ul>
          <ul className="navbar-nav ml-auto">
            {/* @if(User.IsInRole("Admin"))
            {
              <li className="nav-item ml-auto">
                <a
                  asp-area=""
                  asp-controller="User"
                  asp-action="Admin"
                  className="nav-link"
                >
                  Admin
                </a>
              </li>
            } */}
            <li className="nav-item ml-auto">
              <a href="/konto" className="nav-link">
                Konto
              </a>
            </li>
            <li className="nav-item ml-auto ml-lg-1">
              <a href="#" onClick={this.handleLogout} className="nav-link">
                Wyloguj
              </a>
            </li>
          </ul>
        </React.Fragment>
      );
    }
  }
  isAuthenticated() {
    let result = fetch("api/user/check");
    if (result.status === 200) {
      console.log("Authorized success");
      this.setState({
        collapsed: this.state.collapsed,
        isAuth: true,
        navbar: this.state.navbar,
      });
    } else {
      console.log("User unauthorized");
    }
  }

  handleLogout() {
    document.cookie =
      "token.cookie=DELETED; Path=/; Expires=Thu, 01 Jan 1970 00:00:01 GMT;Domain=localhost";
    document.cookie =
      "Identity.External=DELETED; Path=/; Expires=Thu, 01 Jan 1970 00:00:01 GMT;Domain=localhost";
    window.location.href = "/";
  }

  render() {
    return (
      <header>
        <nav className="navbar navbar-expand-lg navbar-dark bg-dark">
          <a className="navbar-brand" href="/">
            Strona główna
          </a>
          <button
            className="navbar-toggler"
            type="button"
            data-toggle="collapse"
            data-target="#navbarNav"
            aria-controls="navbarNav"
            aria-expanded="false"
            aria-label="Toggle navigation"
          >
            <span className="navbar-toggler-icon"></span>
          </button>
          <div className="collapse navbar-collapse" id="navbarNav">
            {this.renderNavbar()}
          </div>
        </nav>
      </header>
    );
  }
}
