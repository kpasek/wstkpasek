import React, { Component } from "react";
import { NavMenu } from "./NavMenu";
import facebook from "../images/facebook.png";
import logo from "../images/logo.png";
import logoWhite from "../images/logo-white.png";
import logoBlack from "../images/logo-black.png";
import github from "../images/github.png";

export class Layout extends Component {
  static displayName = Layout.name;
  render() {
    return (
      <React.Fragment>
        <div>
          <NavMenu
            isAuthenticated={this.props.isAuthenticated}
            handleLogout={this.props.handleLogout}
          />
          {this.props.children}
        </div>
        <footer className="container-fluid mt-5">
          <div className="container">
            <div className="row">
              <div
                className="col-md-3 footer1 d-flex wow bounceInLeft"
                data-wow-delay=".25s"
              >
                <div className="d-flex flex-wrap align-content-center">
                  {" "}
                  <a href="/" className="mt-5">
                    <img src={logo} alt="logo" width="250" />
                  </a>
                  <p>
                    Aplikacja jest częścią pracy dyplomowej pisanej na WST
                    Katowice.
                  </p>
                  <p>
                    &copy; {new Date().getFullYear()} Kamil Pasek.
                    <br /> Wszelkie prawa zastrzeżone.
                  </p>
                </div>
              </div>
              <div
                className="col-md-6 footer2 wow bounceInUp"
                data-wow-delay=".25s"
                id="contact"
              >
                <div className="form-box">
                  <h4>KONTAKT</h4>
                  <table className="table table-responsive d-table">
                    <tr>
                      <td>
                        <input
                          type="text"
                          className="form-control pl-0"
                          placeholder="IMIĘ"
                        />
                      </td>
                      <td>
                        <input
                          type="email"
                          className="form-control pl-0"
                          placeholder="EMAIL"
                        />
                      </td>
                    </tr>
                    <tr>
                      <td colSpan="2"></td>
                    </tr>
                    <tr>
                      <td colSpan="2">
                        <input
                          type="text"
                          className="form-control pl-0"
                          placeholder="TEMAT"
                        />
                      </td>
                    </tr>
                    <tr>
                      <td colSpan="2"></td>
                    </tr>
                    <tr>
                      <td colSpan="2">
                        <input
                          type="text"
                          className="form-control pl-0"
                          placeholder="WIADOMOŚĆ"
                        />
                      </td>
                    </tr>
                    <tr>
                      <td colSpan="2"></td>
                    </tr>
                    <tr>
                      <td colSpan="2" className="text-center pl-0">
                        <button type="submit" className="btn btn-dark">
                          WYŚLIJ
                        </button>
                      </td>
                    </tr>
                  </table>
                </div>
              </div>
              <div
                className="col-md-3 footer3 wow bounceInRight"
                data-wow-delay=".25s"
              >
                <h5 className="mt-5">AUTOR</h5>
                <p>KAMIL PASEK</p>
                <h5>EMAIL</h5>
                <p>KAMILPASEK@GMAIL.COM</p>
                <div className="social-links">
                  <ul className="nav nav-item">
                    <li>
                      <a
                        href="https://www.facebook.com/kamilpaso/"
                        className="btn btn-secondary mr-1 mb-2"
                      >
                        <img src={facebook} alt="facebook" />
                      </a>
                    </li>
                    <li>
                      <a
                        href="https://github.com/kpasek"
                        className="btn btn-secondary mr-1 ml-1 mb-2"
                      >
                        <img
                          src={github}
                          alt="github"
                          width="25"
                          height="auto"
                        />
                      </a>
                    </li>
                  </ul>
                </div>
              </div>
            </div>
          </div>
        </footer>
      </React.Fragment>
    );
  }
}
