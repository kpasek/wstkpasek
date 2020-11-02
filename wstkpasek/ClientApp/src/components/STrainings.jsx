import React, { Component } from "react";
import { SExercise } from "./SExercise";
import { Link } from "react-router-dom";

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
    const parts = await responseParts.json();
    const exercisesRespone = await fetch(
      "api/exercises/part/" + parts[0].name,
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
    const training = await trainingRespone.json();
    const sExercises = await sExercisesRespone.json();
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
        onSwap={this.handleSwapExercise}
      />
    ));
  }
  handleSwapExercise = async (scheduleExerciseId) => {
    const select = document.getElementById(
      "select-exercises-" + scheduleExerciseId
    );
    await fetch("api/schedule/exercises/swap", {
      method: "POST",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        scheduleExerciseId: parseInt(scheduleExerciseId),
        exerciseId: parseInt(select.value),
      }),
    });
    await this.fetchData();
  };
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
  handleEditTraining = async () => {
    const nameInput = document.getElementById("edit-training-name").value;
    const dateInput = document.getElementById("edit-training-date").value;
    const numberInput = document.getElementById("edit-training-number").value;
    const updateTask = fetch(
      "api/schedule/trainings/" + this.state.scheduleTrainingId,
      {
        method: "PUT",
        mode: "cors",
        cache: "no-cache",
        credentials: "include",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          scheduleTrainingId: parseInt(this.state.scheduleTrainingId),
          name: nameInput,
          trainingDate: dateInput,
          exerciseNumber: parseInt(numberInput),
          trainingFinishDate: this.state.training.trainingFinishDate,
          finish: this.state.training.finish,
        }),
      }
    );
    await updateTask;
    await this.fetchData();
  };
  handleAddExercise = async () => {
    const selectExercise = document.getElementById("select-exercise").value;
    const exerciseReponse = fetch("api/schedule/exercises", {
      method: "POST",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        scheduleTrainingId: parseInt(this.state.scheduleTrainingId),
        exerciseId: parseInt(selectExercise),
      }),
    });
    await exerciseReponse;
    await this.fetchData();
  };
  handlePartChange = async () => {
    const selectPart = document.getElementById("select-part").value;

    const getExerciseSelectResponse = await fetch(
      "api/exercises/part/" + selectPart,
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
    const exercises = await getExerciseSelectResponse.json();
    this.setState({
      exerciseSelect: exercises,
    });
  };
  handleDelete = async (exerciseId) => {
    const deleteTask = fetch("api/schedule/exercises/" + exerciseId, {
      method: "DELETE",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
    });
    await deleteTask;
    const newExercises = this.state.exercises.filter(
      (e) => e.scheduleExerciseId !== exerciseId
    );
    this.setState({
      exercises: newExercises,
    });
  };
  handleDeleteTraining = async () => {
    const deleteTask = fetch(
      "api/schedule/trainings/" + this.state.scheduleTrainingId,
      {
        method: "DELETE",
        mode: "cors",
        cache: "no-cache",
        credentials: "include",
        headers: {
          "Content-Type": "application/json",
        },
      }
    );
    await deleteTask;
    document.location.href = "/harmonogram";
  };
  renderSelectPart() {
    return (
      <React.Fragment>
        {this.state.parts.map((part) => (
          <option key={part.name} value={part.name}>
            {part.name}
          </option>
        ))}
      </React.Fragment>
    );
  }
  renderSelectExercise() {
    return (
      <React.Fragment>
        {this.state.exerciseSelect.map((exercise) => (
          <option key={exercise.exerciseId} value={exercise.exerciseId}>
            {exercise.name}
          </option>
        ))}
      </React.Fragment>
    );
  }
  renderBody() {
    if (this.state.loading) {
      return <div>Trwa ładowanie</div>;
    } else {
      return (
        <React.Fragment>
          <div className="col-lg-8 mx-auto">
            <div className="text-right mr-4">
              <Link
                to={"/start/" + this.state.scheduleTrainingId}
                className="text-dark font-size-18"
              >
                Start <i className="icon-play-outline" />
              </Link>
              <i
                className="icon-edit font-size-large"
                type="button"
                data-toggle="collapse"
                data-target={"#edit-training"}
                aria-expanded="false"
                aria-controls={"edit-training"}
              ></i>
              <i
                className="icon-trash-empty color-red font-size-large"
                type="button"
                data-toggle="collapse"
                data-target={"#delete-training"}
                aria-expanded="false"
                aria-controls={"delete-training"}
              ></i>
            </div>
            <h2 className="text-center mb-2">{this.state.training.name}</h2>
            <div className="text-center mt-3 mb-2 training-subtitle">
              {new Date(this.state.training.trainingDate).toLocaleString()}
            </div>
            <div className="collapse" id="edit-training">
              <h3>Edytuj trening</h3>
              <label htmlFor="edit-training-name" className="mt-1">
                Nazwa:
              </label>
              <input
                type="text"
                id="edit-training-name"
                className="form-control mt-1"
                defaultValue={this.state.training.name}
              />
              <label htmlFor="edit-training-date" className="mt-1">
                Data:
              </label>
              <input
                type="datetime-local"
                className="form-control mt-1"
                id="edit-training-date"
                defaultValue={this.state.training.trainingDate}
              />
              <label htmlFor="edit-training-number" className="mt-1">
                Ilość podpowiadanych ćwiczeń
              </label>
              <input
                type="number"
                className="form-control mt-1"
                id="edit-training-number"
                defaultValue={this.state.training.exerciseNumber}
              />
              <button
                className="btn btn-outline-primary mt-2"
                onClick={this.handleEditTraining}
              >
                Zatwierdź
              </button>
            </div>
            <div className="collapse" id="delete-training">
              <p>Usunięcie treningu jest nieodwracalne</p>
              <button
                className="btn btn-outline-danger mt-2"
                onClick={this.handleDeleteTraining}
              >
                Usuń trening
              </button>
            </div>
            <div className="mx-auto">
              <div className="">{this.renderExercises()}</div>
              <a
                href="#add-sExercise"
                className="link-black  font-size-large mt-3"
                type="button"
                data-toggle="collapse"
                data-target={"#add-sExercise"}
                aria-expanded="false"
                aria-controls={"add-sExercise"}
              >
                <i className="icon-plus" />
                <span className="ml-2">Dodaj ćwiczenie</span>
              </a>

              <div id="add-sExercise" className="collapse mt-3">
                <label className="mt-1 font-size-18" htmlFor="select-part">
                  Partia:
                </label>
                <select
                  id="select-part"
                  className="custom-select"
                  onChange={this.handlePartChange}
                >
                  {this.renderSelectPart()}
                </select>
                <label className="mt-1 font-size-18" htmlFor="select-exercise">
                  Ćwiczenie:
                </label>
                <select id="select-exercise" className="custom-select mt-1">
                  {this.renderSelectExercise()}
                </select>
                <button
                  className="btn btn-outline-success mt-2"
                  onClick={this.handleAddExercise}
                >
                  Dodaj ćwiczenie
                </button>
              </div>
            </div>
          </div>
        </React.Fragment>
      );
    }
  }
  render() {
    return this.renderBody();
  }
}
