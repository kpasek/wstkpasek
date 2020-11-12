import React, { Component } from "react";
import { Redirect } from "react-router";
import calendar from "../images/calendar.png";
import trImg from "../images/home-training.png";
import chart from "../images/chart-450x300.png";

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
            className="btn text-uppercase btn-outline-warning btn-lg mr-3 mb-3 mt-2"
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
            className="btn text-uppercase btn-outline-warning btn-lg mr-3 mb-3 mt-2"
          >
            {" "}
            ZALOGUJ
          </a>
          <a
            href="#"
            onClick={this.handleTestUser}
            className="btn text-uppercase btn-outline-warning btn-lg mr-3 mb-3 mt-2"
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
        <div className="text-right">
          {" "}
          WITAJ PONOWNIE <br />
          <span>ZACZYNAJMY</span>{" "}
        </div>
      );
    } else {
      return (
        <div className="text-right">
          {" "}
          DOŁĄCZ DO NAS <br />
        </div>
      );
    }
  }
  render() {
    if (this.state.redirect) {
      return <Redirect to={this.state.redirect} />;
    } else {
      return (
        <React.Fragment>
          <div id="top-background">
            <div className="container">
              <div className="row">
                <div className="col-12">
                  <div className="float-right mt-5 hello-section p-3">
                    {this.renderHello()}
                    <div className="float-right">{this.renderLinks()}</div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div className="container mt-5">
            <div className="row">
              <div className="col-md-6 mt-4 text-right">
                <h1 className="home-bottom">Zaplanuj</h1>
                <p className="text-justify">
                  Lorem ipsum dolor sit amet, consectetur adipiscing elit.
                  Phasellus tempor et ex vel hendrerit. Duis feugiat risus vitae
                  justo convallis viverra at sit amet nulla. Mauris nec sodales
                  enim, eget vestibulum purus. Mauris eu rutrum felis. Curabitur
                  pellentesque augue non est commodo, eu pretium erat placerat.
                  Etiam libero purus, convallis at urna quis, imperdiet
                  venenatis dui. Aenean ornare tortor ac egestas placerat.
                </p>
              </div>
              <div className="col-md-6 mt-4">
                <img src={calendar} alt="" className="img-width" />
              </div>
              <div id="home-left2" className="col-md-6 mt-4">
                <img
                  src={trImg}
                  alt=""
                  id="home-right"
                  className="float-right img-width"
                />
                <div className="clearfix"></div>
              </div>
              <div className="col-md-6 mt-4">
                <h1 className="home-bottom">Wykonaj</h1>
                <p className="text-justify">
                  Lorem ipsum dolor sit amet, consectetur adipiscing elit.
                  Phasellus tempor et ex vel hendrerit. Duis feugiat risus vitae
                  justo convallis viverra at sit amet nulla. Mauris nec sodales
                  enim, eget vestibulum purus. Mauris eu rutrum felis. Curabitur
                  pellentesque augue non est commodo, eu pretium erat placerat.
                  Etiam libero purus, convallis at urna quis, imperdiet
                  venenatis dui. Aenean ornare tortor ac egestas placerat.
                </p>
              </div>
              <div id="home-left3" className="col-md-6 mt-4 text-right">
                <h1 className="home-bottom">Obserwuj</h1>
                <p className="text-justify">
                  Lorem ipsum dolor sit amet, consectetur adipiscing elit.
                  Phasellus tempor et ex vel hendrerit. Duis feugiat risus vitae
                  justo convallis viverra at sit amet nulla. Mauris nec sodales
                  enim, eget vestibulum purus. Mauris eu rutrum felis. Curabitur
                  pellentesque augue non est commodo, eu pretium erat placerat.
                  Etiam libero purus, convallis at urna quis, imperdiet
                  venenatis dui. Aenean ornare tortor ac egestas placerat.
                </p>
              </div>
              <div id="home-right3" className="col-md-6 mt-4">
                <img src={chart} alt="chart" className="img-width" />
              </div>
            </div>
          </div>
        </React.Fragment>
      );
    }
  }
}
