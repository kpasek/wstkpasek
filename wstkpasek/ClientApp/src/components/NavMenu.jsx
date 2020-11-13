/* eslint-disable jsx-a11y/anchor-is-valid */
import React, { Component } from "react";
import { Link } from "react-router-dom";
import "./NavMenu.css";
import logo from "../images/logo.png";

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
  hideNavbar() {
    document.getElementById("navbar-toggler-button").click();
  }
  componentDidUpdate() {}
  renderNavbar() {
    if (!this.props.isAuthenticated) {
      return (
        <React.Fragment>
          <ul className="navbar-nav ml-auto">
            <li className="nav-item">
              <Link to="/prywatnosc">
                <span className="nav-link" onClick={this.hideNavbar}>
                  Prywatność
                </span>
              </Link>
            </li>
            <li className="nav-item">
              <Link to="/logowanie">
                <span className="nav-link" onClick={this.hideNavbar}>
                  Zaloguj/Zarejestruj
                </span>
              </Link>
            </li>
          </ul>
        </React.Fragment>
      );
    } else {
      return (
        <React.Fragment>
          <ul className="navbar-nav ml-auto">
            <li className="nav-item">
              <Link to="/harmonogram" onClick={this.hideNavbar}>
                <span className="nav-link text-uppercase" href="/harmonogram">
                  Harmonogram
                </span>
              </Link>
            </li>
            <li className="nav-item">
              <Link to="/trening" onClick={this.hideNavbar}>
                <span className="nav-link text-uppercase">Treningi</span>
              </Link>
            </li>
            <li className="nav-item">
              <Link to="/cwiczenia" onClick={this.hideNavbar}>
                <span className="nav-link text-uppercase">Lista ćwiczeń</span>
              </Link>
            </li>
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
              <Link to="/postepy" onClick={this.hideNavbar}>
                <span className="nav-link text-uppercase">Postępy</span>
              </Link>
            </li>
            <li className="nav-item ml-auto">
              <Link to="/konto" onClick={this.hideNavbar}>
                <span className="nav-link text-uppercase">Konto</span>
              </Link>
            </li>
            <li className="nav-item ml-auto ml-lg-1">
              <Link to="#" onClick={this.props.handleLogout}>
                <span className="nav-link cursor-pointer text-uppercase">
                  WYLOGUJ
                </span>
              </Link>
            </li>
          </ul>
        </React.Fragment>
      );
    }
  }

  render() {
    return (
      <header>
        <nav className="navbar navbar-expand-lg navbar-dark bg-black">
          <div className="container">
            <a className="navbar-brand mr-auto" href="/">
              <img src={logo} alt="Workout Planner" height="auto" width="190" />
            </a>

            <button
              className="navbar-toggler"
              id="navbar-toggler-button"
              type="button"
              data-toggle="collapse"
              data-target="#collapsibleNavbar"
            >
              {" "}
              <span className="navbar-toggler-icon"></span>{" "}
            </button>

            <div className="collapse navbar-collapse" id="collapsibleNavbar">
              {this.renderNavbar()}
            </div>
          </div>
        </nav>
      </header>
    );
  }
}
