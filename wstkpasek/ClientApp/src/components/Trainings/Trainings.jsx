import React, { Component } from "react";
import { Training } from "./Training";

import "../../css/site.css";

export class Trainings extends Component {
  static displayName = Trainings.name;

  constructor(props) {
    super(props);
    this.state = { trainings: [], loading: true };
  }

  async componentDidMount() {
    await this.fetchTrainingData();
  }

  async fetchTrainingData() {
    const response = await fetch("/api/trainings", {
      method: "GET",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
    });
    const data = await response.json();
    this.setState({ trainings: data, loading: false });
  }
  renderBody() {
    if (this.state.loading) return <h2>Trwa ładowanie...</h2>;
    return (
      <React.Fragment>
        <div className="mt-3 col-lg-8 p-0">
          {this.state.trainings.map((training) => (
            <Training
              key={training.trainingId}
              trainingId={training.trainingId}
            />
          ))}
        </div>
      </React.Fragment>
    );
  }
  render() {
    return this.renderBody();
  }
}
