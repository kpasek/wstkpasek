import React, { Component } from "react";
import ExerciseDetail from "../Exercises/ExerciseDetail";

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
  handleChangeExercise = async (exerciseId, order) => {
    var select = document.getElementById("select-exercise-" + exerciseId);
    await fetch("/api/exercises/change/" + exerciseId, {
      method: "POST",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        exerciseId: parseInt(select.value),
        trainingId: this.state.trainingId,
        order: order,
      }),
    });
    await this.fetchData();
  };
  renderPartsSelect = () => {
    return (
      <React.Fragment>
        {this.state.parts.map((part) => (
          <option key={"select-" + part.name} value={part.name}>
            {part.name}
          </option>
        ))}
      </React.Fragment>
    );
  };
  handleChangePart = async () => {
    const select = document.getElementById(
      "select-training-part-" + this.state.trainingId
    );
    const getResult = await fetch("api/exercises/part/" + select.value, {
      method: "GET",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
    });
    const exercises = await getResult.json();
    this.setState({
      selectExercises: exercises,
    });
  };
  renderExercisesSelect = () => {
    return (
      <React.Fragment>
        {this.state.selectExercises.map((exercise) => (
          <option
            key={"select-" + exercise.exerciseId}
            value={exercise.exerciseId}
          >
            {exercise.name}
          </option>
        ))}
      </React.Fragment>
    );
  };
  handleAddExercise = async () => {
    const select = document.getElementById(
      "select-training-exercise-" + this.state.trainingId
    );
    await fetch("api/trainings/exercise/add", {
      method: "POST",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        trainingId: this.state.trainingId,
        exerciseId: parseInt(select.value),
      }),
    });
    await this.fetchData();
  };
  handleDeleteExercise = async (exerciseId, order) => {
    await fetch("api/trainings/exercise/delete", {
      method: "DELETE",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        trainingId: this.state.trainingId,
        exerciseId: exerciseId,
        order: order,
      }),
    });
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
    const gotParts = await fetch("api/exercises/parts", {
      method: "GET",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
    });
    const parts = await gotParts.json();
    const gotExercises = await fetch("api/exercises", {
      method: "GET",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
    });
    const exercises = await gotExercises.json();

    this.setState({
      trainingId: this.state.trainingId,
      exercises: data,
      parts: parts,
      selectExercises: exercises,
      loading: false,
    });
  };
  renderBody = () => {
    if (this.state.loading) {
      return (
        <h2>
          Trwa ładowanie <i className="icon-spin4"></i>
        </h2>
      );
    } else {
      return (
        <React.Fragment>
          <div>
            <div className="ml-1">
              {this.state.exercises.map((exercise) => (
                <ExerciseDetail
                  key={"exercise-component-" + exercise.exerciseId + "-"}
                  training={true}
                  trainingId={this.state.trainingId}
                  exercise={exercise}
                  types={this.props.types}
                  parts={this.props.parts}
                  refresh={this.fetchData}
                  handleChangeExercise={this.handleChangeExercise}
                  onDeleteExercise={this.handleDeleteExercise}
                />
              ))}
            </div>
            {/* add button */}
            <div className="mt-2">
              <a
                href={"#training-add-exercise-" + this.state.trainingId}
                className="link-black"
                data-toggle="collapse"
                role="button"
                aria-expanded="false"
                aria-controls="collapseAddExercise"
              >
                <span className="font-size-20">
                  <i className="icon-plus"></i> Dodaj
                </span>
              </a>
            </div>
            <div
              className="collapse"
              id={"training-add-exercise-" + this.state.trainingId}
            >
              <select
                className="custom-select my-1"
                id={"select-training-part-" + this.state.trainingId}
                onChange={this.handleChangePart}
              >
                {this.renderPartsSelect()}
              </select>
              <select
                className="custom-select"
                id={"select-training-exercise-" + this.state.trainingId}
              >
                {this.renderExercisesSelect()}
              </select>
              <button
                className="btn btn-outline-primary my-2"
                onClick={this.handleAddExercise}
              >
                Dodaj ćwiczenie
              </button>
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
