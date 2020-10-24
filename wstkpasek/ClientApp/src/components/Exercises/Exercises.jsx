import React, { Component } from "react";
import ExerciseDetail from "./ExerciseDetail";
// cosnt exercisesResponse = await fetch("/api/exercises", {
//   method: "GET",
//   mode: "cors",
//   cache: "no-cache",
//   credentials: "include",
//   headers: {
//     "Content-Type": "application/json",
//   },
//   body: JSON.stringify({
//     exerciseId: parseInt(select.value),
//     trainingId: this.state.trainingId,
//     order: order,
//   }),
// });
export class Exercises extends Component {
  static displayName = Exercises.name;

  constructor(props) {
    super(props);
    this.state = {
      parts: [],
      types: [],
      exercises: [],
      exercisesCopy: [],
      loading: true,
    };
  }

  componentDidMount() {
    this.fetchData();
  }
  fetchData = async () => {
    const exercisesResponse = await fetch("/api/exercises", {
      method: "GET",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
    });
    const responseParts = await fetch("api/exercises/parts", {
      method: "GET",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
    });
    const responseTypes = await fetch("api/exercises/types", {
      method: "GET",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
    });
    const exercises = await exercisesResponse.json();
    const parts = await responseParts.json();
    const types = await responseTypes.json();

    this.setState({
      parts: parts,
      types: types,
      exercises: exercises,
      exercisesCopy: exercises,
      loading: false,
    });
  };

  handleDeleteExercise = async (exerciseId, order) => {
    await fetch("api/exercises/" + exerciseId, {
      method: "DELETE",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
    });
    this.setState({
      exercises: this.state.exercises.filter(
        (e) => e.exerciseId !== exerciseId
      ),
    });
  };
  renderSelectPart = () => {
    return (
      <select
        className="custom-select my-2"
        id={"select-exercise-part"}
        onChange={this.handleChangePart}
      >
        {this.renderPartsOptions()}
      </select>
    );
  };
  renderPartsOptions = () => {
    return (
      <React.Fragment>
        <option key="select-default" value="all">
          Wybierz partię
        </option>
        {this.state.parts.map((part) => (
          <option key={"select-" + part.name} value={part.name}>
            {part.name}
          </option>
        ))}
      </React.Fragment>
    );
  };
  handleChangePart = async () => {
    const part = document.getElementById("select-exercise-part").value;

    if (part === "all") {
      const exercisesResponse = await fetch("/api/exercises", {
        method: "GET",
        mode: "cors",
        cache: "no-cache",
        credentials: "include",
        headers: {
          "Content-Type": "application/json",
        },
      });
      this.setState({
        exercises: await exercisesResponse.json(),
      });
    } else {
      const responseExercise = await fetch("api/exercises/part/" + part, {
        method: "GET",
        mode: "cors",
        cache: "no-cache",
        credentials: "include",
        headers: {
          "Content-Type": "application/json",
        },
      });
      this.setState({
        exercises: await responseExercise.json(),
      });
    }
  };
  renderPartTypeList() {
    return (
      <React.Fragment>
        <datalist id="part-list">
          {this.state.parts.map((part) => (
            <option key={part.name} value={part.name}>
              {part.name}
            </option>
          ))}
        </datalist>
        <datalist id="type-list">
          {this.state.types.map((type) => (
            <option key={type.typeName} value={type.typeName}>
              {type.typeName}
            </option>
          ))}
        </datalist>
      </React.Fragment>
    );
  }
  handleNewExercise = async () => {
    const name = document.getElementById("new-exercise-name").value;
    const part = document.getElementById("new-exercise-part").value;
    const type = document.getElementById("new-exercise-type").value;
    const description = document.getElementById("new-exercise-description")
      .value;

    await fetch("api/exercises", {
      method: "POST",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        name: name,
        partId: part,
        typeId: type,
        description: description,
      }),
    });
    this.fetchData();
  };
  renderBody = () => {
    if (this.state.loading) {
      return (
        <div>
          Trwa ładowanie <i className="icon-spin4"></i>
        </div>
      );
    } else {
      return (
        <React.Fragment>
          <i
            className="icon-plus font-size-20 float-left pl-3 pt-2"
            type="button"
            data-toggle="collapse"
            data-target={"#new-exercise"}
            aria-expanded="false"
            aria-controls={"new-exercise"}
          ></i>
          <h1 className="mt-3 mb-4 text-center">Ćwiczenia</h1>
          <div style={{ clear: "both" }}></div>
          <div
            id={"new-exercise"}
            className="collapse"
            aria-labelledby="newExercise"
          >
            <h3>Nowe ćwiczenie</h3>
            <input
              type="text"
              className="form-control mt-1"
              id="new-exercise-name"
              placeholder="Nazwa ćwiczenia"
              required
            ></input>
            <input
              type="text"
              className="form-control mt-1"
              id="new-exercise-part"
              list="part-list"
              placeholder="Partia"
              required
            ></input>
            <input
              type="text"
              className="form-control mt-1"
              id="new-exercise-type"
              placeholder="Typ ćwiczenia"
              list="type-list"
            ></input>
            <input
              type="text"
              className="form-control mt-1"
              id="new-exercise-description"
              placeholder="Opis ćwiczenia"
            ></input>
            <button
              className="btn btn-outline-primary mt-2"
              onClick={this.handleNewExercise}
            >
              Zatwierdź
            </button>
          </div>
          {this.renderSelectPart()}
          {this.state.exercises.map((exercise) => (
            <ExerciseDetail
              key={"exercise-component-" + exercise.exerciseId + "-"}
              training={false}
              exercise={exercise}
              types={this.state.types}
              parts={this.state.parts}
              onDeleteExercise={this.handleDeleteExercise}
              refresh={this.fetchData}
            />
          ))}
          {this.renderPartTypeList()}
        </React.Fragment>
      );
    }
  };

  render() {
    return this.renderBody();
  }
}
