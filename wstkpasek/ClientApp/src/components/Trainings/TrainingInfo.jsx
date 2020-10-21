import React, { Component } from "react";

export default class TrainingInfo extends Component {
  constructor(props) {
    super(props);

    this.state = {
      trainingId: this.props.trainingId,
      exercises: [],
      loading: true,
    };
  }
  componentDidMount = async () => {
    const response = await fetch(
      "/api/exercises/training/" + this.state.trainingId,
      {
        method: "GET",
        mode: "cors",
        cache: "no-cache",
        credentials: "include",
        headers: {
          "Content-Type": "application/json",
        },
      }
    );
    const data = await response.json();
    this.setState({
      trainingId: this.state.trainingId,
      exercises: data,
      loading: false,
    });
  };
  renderBody = () => {
    if (this.state.loading) {
      return <h2>Trwa ładowanie</h2>;
    } else {
      return (
        <React.Fragment>
          <div>
            <p>Lista ćwiczeń:</p>
            <div className="ml-1">
              {this.state.exercises.map((exercise) => (
                <div key={exercise.exerciseId}>{exercise.name}</div>
              ))}
            </div>
          </div>
        </React.Fragment>
      );
    }
  };
  render() {
    return this.renderBody();
  }
}
