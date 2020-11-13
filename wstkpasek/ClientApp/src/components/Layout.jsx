import React, { Component } from "react";
import { NavMenu } from "./NavMenu";
import logo from "../images/logo.png";
import facebook from "../images/facebook.png";
import github from "../images/github.png";

export class Layout extends Component {
  static displayName = Layout.name;
  render() {
    return (
      <React.Fragment>
        <div id="page-content">
          <NavMenu
            isAuthenticated={this.props.isAuthenticated}
            handleLogout={this.props.handleLogout}
          />
          {this.props.children}
        </div>
        <footer className="footer">
          <div className="container-fluid bg-black" id="myFooter">
            <div className="row">
              <div className="col-8 mx-auto">
                <div className="row">
                  <div className="col-md-3 mt-3 mx-auto">
                    <div className="float-right">
                      <a href="/">
                        <img src={logo} alt="" width="300" />
                      </a>
                      <div className="mt-1 py-4">
                        <p className="color-white">
                          Aplikacja jest częścią pracy dyplomowej pisanej na WST
                          Katowice.
                        </p>
                        <p className="color-white">
                          &copy; {new Date().getFullYear()} Wszeklie prawa
                          zastrzeżone.{" "}
                        </p>
                      </div>
                    </div>
                    <div className="clearfix"></div>
                  </div>
                  <div className="col-md-3 mx-auto">
                    <h3 className="text-uppercase mt-3">Kontakt</h3>
                    <div className="text-uppercase py-2">EMAIL</div>
                    <a
                      href="mailto:kamilpasek@gmail.com"
                      className="link-white"
                    >
                      kamilpasek@gmail.com
                    </a>
                    <div className="text-uppercase py-2">SOCIAL</div>
                    <div className="mb-4">
                      <a href="https://www.facebook.com/kamilpaso/">
                        <img
                          src={facebook}
                          alt="facebook"
                          className="social-icon p-2"
                          width="45"
                        />
                      </a>
                      <a href="https://github.com/kpasek">
                        <img
                          src={github}
                          alt="github"
                          className="social-icon p-2 ml-1"
                          width="45"
                        />
                      </a>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </footer>
      </React.Fragment>
    );
  }
}
