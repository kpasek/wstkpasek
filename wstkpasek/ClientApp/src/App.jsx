import React, { Component } from "react";
import { Route } from "react-router";
import { Layout } from "./components/Layout";
import { Home } from "./components/Home";
import { Trainings } from "./components/Trainings/Trainings";
import { Exercises } from "./components/Exercises/Exercises";
import { Series } from "./components/Series";
import { Schedule } from "./components/Schedule";
import { STrainings } from "./components/STrainings";
import { SExercise } from "./components/SExercise";
import { SSeries } from "./components/SSeries";
import { RunTraining } from "./components/RunTraining";
import { User } from "./components/User";
import { Admin } from "./components/Admin";
import { Account } from "./components/Account";

import "./css/site.css";

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
  handleLogin = () => {
    this.setState({ isAuthenticated: true });
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
        <Route path="/trening" exact component={Trainings} />
        <Route path="/cwiczenia" component={Exercises} />
        <Route path="/seria" component={Series} />
        <Route path="/harmonogram" exact component={Schedule} />
        <Route path="/harmonogram/:trainingId" exact component={STrainings} />
        <Route path="/harmonogram/:trainingId/start" component={RunTraining} />
        <Route path="/konto" component={Account} />
        <Route
          path="/logowanie"
          render={(props) => <User handleLogin={this.handleLogin} {...props} />}
        />
        <Route path="/admin" component={Admin} />
      </Layout>
    );
  }
}
