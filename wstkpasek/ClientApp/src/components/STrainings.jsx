import React, { Component } from "react";
import { SExercise } from "./SExercise";

export class STrainings extends Component {
  static displayName = STrainings.name;

  constructor(props) {
    super(props);
    this.state = {
      scheduleTrainingId: this.props.match.params.trainingId,
      loading: true,
    };
  }

  componentDidMount() {
    this.fetchData();
  }
  fetchData = async () => {
    const trainingRespone = await fetch(
      "api/schedule/trainings/" + this.state.scheduleTrainingId,
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
    const sExercisesRespone = await fetch(
      "api/schedule/exercises/t/" + this.state.scheduleTrainingId,
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
    const responseParts = await fetch("api/exercises/parts", {
      method: "GET",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
    });
    const exercisesRespone = await fetch("api/exercises", {
      method: "GET",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
    });
    const training = await trainingRespone.json();
    const sExercises = await sExercisesRespone.json();
    const parts = await responseParts.json();
    const exercises = await exercisesRespone.json();
    this.setState({
      training: training,
      exercises: sExercises,
      exerciseSelect: exercises,
      parts: parts,
      loading: false,
    });
  };
  renderExercises() {
    return this.state.exercises.map((exercise) => (
      <SExercise
        key={exercise.scheduleExerciseId}
        exercise={exercise}
        onEdit={this.handleEdit}
        onDelete={this.handleDelete}
      />
    ));
  }
  handleEdit = async (exerciseId, sExerciseId) => {
    const newName = document.getElementById("edit-exercise-name-" + sExerciseId)
      .value;
    const newPart = document.getElementById("edit-exercise-part-" + sExerciseId)
      .value;
    const newType = document.getElementById("edit-exercise-type-" + sExerciseId)
      .value;
    const newDescription = document.getElementById(
      "edit-exercise-description-" + sExerciseId
    ).value;
    let newOrder = document.getElementById("edit-exercise-order-" + sExerciseId)
      .value;

    const updateTask = fetch("api/exercises/" + exerciseId, {
      method: "PUT",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        exerciseId: exerciseId,
        name: newName,
        partId: newPart,
        typeId: newType,
        description: newDescription,
        public: this.state.public,
      }),
    });
    const changeOrderTask = fetch("api/schedule/exercises/change-order", {
      method: "POST",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        scheduleExerciseId: sExerciseId,
        order: parseInt(newOrder),
      }),
    });
    await updateTask;
    await changeOrderTask;
    await this.fetchData();
  };
  renderBody() {
    if (this.state.loading) {
      return <div>Trwa ładowanie</div>;
    } else {
      return (
        <React.Fragment>
          <h2 className="text-center mt-3 mb-2">{this.state.training.name}</h2>
          <div className="text-center mt-3 mb-2 training-subtitle">
            {new Date(this.state.training.trainingDate).toLocaleString()}
          </div>
          <div className="col-lg-8 mx-auto">
            <div className="">{this.renderExercises()}</div>
          </div>
        </React.Fragment>
      );
    }
  }
  render() {
    return this.renderBody();
  }
}
