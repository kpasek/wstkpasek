import React, { Component } from "react";
import { Redirect } from "react-router";

export class Home extends Component {
  static displayName = Home.name;
  constructor(props) {
    super(props);
    this.state = {
      redirect: false,
      loading: true,
    };
  }
  handleTestUser = async () => {
    fetch("/api/user/login", {
      method: "POST",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        Email: "test@test.pl",
        Password: "test123",
      }),
    }).then((response) => {
      if (response.ok) {
        this.props.handleLogin();
        this.setState({
          redirect: "/harmonogram",
        });
      }
    });
  };
  renderLinks() {
    if (this.props.isAuthenticated) {
      return (
        <React.Fragment>
          <a
            href="/harmonogram"
            className="btn text-uppercase btn-outline-danger btn-lg mr-3 mb-3 wow bounceInUp"
          >
            HARMONOGRAM
          </a>
        </React.Fragment>
      );
    } else {
      return (
        <React.Fragment>
          <a
            href="/logowanie"
            className="btn text-uppercase btn-outline-danger btn-lg mr-3 mb-3 wow bounceInUp"
          >
            {" "}
            ZALOGUJ
          </a>
          <a
            href="#"
            onClick={this.handleTestUser}
            className="btn text-uppercase btn-outline-danger btn-lg mb-3 wow bounceInDown"
          >
            DEMO
          </a>
        </React.Fragment>
      );
    }
  }
  renderHello() {
    if (this.props.isAuthenticated) {
      return (
        <h2>
          {" "}
          WITAJ PONOWNIE <br />
          <span>ZACZYNAJMY</span>{" "}
        </h2>
      );
    } else {
      return (
        <h2>
          {" "}
          DOŁĄCZ DO NAS <br />
          <span>ZACZYNAJMY</span>{" "}
        </h2>
      );
    }
  }
  render() {
    if (this.state.redirect) {
      return <Redirect to={this.state.redirect} />;
    } else {
      return (
        <div id="top-background">
          <div className="container">
            <div className="fh5co-banner-text-box">
              <div className="quote-box pl-5 pr-5 wow bounceInRight">
                {this.renderHello()}
              </div>
              {this.renderLinks()}
              <div className="clearfix"></div>
            </div>
          </div>
        </div>
      );
    }
  }
}
