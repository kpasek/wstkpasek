/* eslint-disable jsx-a11y/anchor-is-valid */
import React, { Component } from "react";
import "./NavMenu.css";

export class NavMenu extends Component {
  static displayName = NavMenu.name;

  constructor(props) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.state = {
      collapsed: true,
    };
  }
  toggleNavbar() {
    this.setState({
      collapsed: !this.state.collapsed,
    });
  }
  componentDidUpdate() {}
  renderNavbar() {
    if (!this.props.isAuthenticated) {
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
              <a href="/logowanie" className="nav-link">
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
              <a
                href="#"
                onClick={this.props.handleLogout}
                className="nav-link"
              >
                Wyloguj
              </a>
            </li>
          </ul>
        </React.Fragment>
      );
    }
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
