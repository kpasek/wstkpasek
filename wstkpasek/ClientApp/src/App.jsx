import React, { Component } from "react";
import { Route } from "react-router";
import { Layout } from "./components/Layout";
import { Home } from "./components/Home";
import { Trainings } from "./components/Trainings/Trainings";
import { Exercises } from "./components/Exercises";
import { Series } from "./components/Series";
import { Schedule } from "./components/Schedule";
import { STrainings } from "./components/STrainings";
import { SExercises } from "./components/SExercises";
import { SSeries } from "./components/SSeries";
import { RunTraining } from "./components/RunTraining";
import { User } from "./components/User";
import { Admin } from "./components/Admin";
import { Account } from "./components/Account";

import "./custom.css";

export default class App extends Component {
  static displayName = App.name;
  constructor() {
    super();
    this.state = {
      isAuthenticated: false,
    };
  }

  isAuthenticated() {
    fetch("api/user/check", {
      method: "GET",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
    }).then((response) => {
      if (response.ok) {
        this.setState({
          isAuthenticated: true,
        });
      }
    });
  }
  handleLogout = () => {
    fetch("api/user/logout").then(() => {
      this.setState({
        isAuthenticated: false,
      });
      window.location.href = "/";
    });
  };
  componentDidMount() {
    this.isAuthenticated();
  }
  render() {
    return (
      <Layout
        isAuthenticated={this.state.isAuthenticated}
        handleLogout={this.handleLogout}
      >
        <Route exact path="/" component={Home} />
        <Route path="/trening" component={Trainings} />
        <Route path="/cwiczenia" component={Exercises} />
        <Route path="/seria" component={Series} />
        <Route path="/harmonogram" component={Schedule} />
        <Route path="/harmonogram/trening" component={STrainings} />
        <Route path="/harmonogram/cwiczenia" component={SExercises} />
        <Route path="/harmonogram/seria" component={SSeries} />
        <Route path="/harmonogram/trening/wykonaj" component={RunTraining} />
        <Route path="/konto" component={Account} />
        <Route path="/logowanie" component={User} />
        <Route path="/admin" component={Admin} />
      </Layout>
    );
  }
}
