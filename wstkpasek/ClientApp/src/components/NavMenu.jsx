/* eslint-disable jsx-a11y/anchor-is-valid */
import React, { Component } from "react";
import { Link } from "react-router-dom";
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
              <Link to="/prywatnosc">
                <span className="nav-link">Prywatność</span>
              </Link>
            </li>
          </ul>
          <ul className="navbar-nav ml-auto">
            <li className="nav-item">
              <Link to="/logowanie">
                <span className="nav-link">Zaloguj/Zarejestruj</span>
              </Link>
            </li>
          </ul>
        </React.Fragment>
      );
    } else {
      return (
        <React.Fragment>
          <ul className="navbar-nav">
            <li className="nav-item">
              <Link to="/harmonogram">
                <span className="nav-link" href="/harmonogram">
                  Harmonogram
                </span>
              </Link>
            </li>
            <li className="nav-item">
              <Link to="/trening">
                <span className="nav-link">Treningi</span>
              </Link>
            </li>
            <li className="nav-item">
              <Link to="/cwiczenia">
                <span className="nav-link">Lista ćwiczeń</span>
              </Link>
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
              <Link to="/konto">
                <span className="nav-link">Konto</span>
              </Link>
            </li>
            <li className="nav-item ml-auto ml-lg-1">
              <span
                onClick={this.props.handleLogout}
                className="nav-link cursor-pointer"
              >
                Wyloguj
              </span>
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
          <Link to="/">
            <span className="navbar-brand">Strona główna</span>
          </Link>
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
