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
  render() {
    if (this.state.redirect) {
      return <Redirect to={this.state.redirect} />;
    } else {
      return (
        <div>
          <h1>Strona w budowie</h1>
          <button
            className="btn btn-outline-success mt-2"
            onClick={this.handleTestUser}
          >
            Zaloguj się na użytkownika testowego
          </button>
        </div>
      );
    }
  }
}
