import React, { Component } from "react";
import ExerciseDetail from "./ExerciseDetail";

export default class TrainingDetails extends Component {
  constructor(props) {
    super(props);

    this.state = {
      trainingId: this.props.trainingId,
      exercises: [],
      loading: true,
    };
  }
  componentDidMount = async () => {
    await this.fetchData();
  };
  fetchData = async () => {
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
            <div className="ml-1">
              {this.state.exercises.map((exercise) => (
                <ExerciseDetail
                  key={"exercise-component-" + exercise.exerciseId}
                  trainingId={this.state.trainingId}
                  exerciseId={exercise.exerciseId}
                  types={this.props.types}
                  parts={this.props.parts}
                  refresh={this.fetchData}
                />
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
