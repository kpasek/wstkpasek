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
  componentDidUpdate() {}
  renderNavbar() {
    if (!this.props.isAuthenticated) {
      return (
        <React.Fragment>
          <ul className="navbar-nav ml-auto">
            <li className="nav-item">
              <Link to="/prywatnosc">
                <span className="nav-link">Prywatność</span>
              </Link>
            </li>
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
          <ul className="navbar-nav ml-auto">
            <li className="nav-item">
              <Link to="/harmonogram">
                <span className="nav-link text-uppercase" href="/harmonogram">
                  Harmonogram
                </span>
              </Link>
            </li>
            <li className="nav-item">
              <Link to="/trening">
                <span className="nav-link text-uppercase">Treningi</span>
              </Link>
            </li>
            <li className="nav-item">
              <Link to="/cwiczenia">
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
              <Link to="/postepy">
                <span className="nav-link text-uppercase">Postępy</span>
              </Link>
            </li>
            <li className="nav-item ml-auto">
              <Link to="/konto">
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
              <img src={logo} alt="Workout Planner" height="15" />
            </a>

            <button
              className="navbar-toggler"
              type="button"
              data-toggle="collapse"
              data-target="#collapsibleNavbar"
            >
              {" "}
              <span className="navbar-toggler-icon"></span>{" "}
            </button>

            <div className="collapse navbar-collapse" id="collapsibleNavbar">
              {this.renderNavbar()}
              {/* <ul className="navbar-nav ml-5">
                <li className="nav-item">
                  {" "}
                  <a className="nav-link btn btn-danger" href="#">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  </a>{" "}
                </li>
              </ul> */}
            </div>
          </div>
        </nav>
        {/* <nav className="navbar navbar-expand-lg navbar-dark bg-dark">
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
        </nav> */}
      </header>
    );
  }
}
