﻿import React, { Component } from "react";
import "../../css/fontello.css";

export class Training extends Component {
  static displayName = Training.name;

  constructor(props) {
    super(props);
    this.state = {
      trainingId: this.props.trainingId,
      name: "",
      exerciseNumber: 2,
      public: false,
      loading: true,
      editValidateMessage: "",
      info: "",
    };
  }

  async componentDidMount() {
    await this.fetchTrainingData();
  }

  handleCloseMessege = () => {
    this.setState({
      trainingId: this.state.trainingId,
      name: this.state.name,
      exerciseNumber: this.state.exerciseNumber,
      public: this.state.public,
      loading: false,
      editValidateMessage: this.state.editValidateMessage,
      info: "",
    });
  };
  handleEdit = async () => {
    const newNameInput = document.getElementById(
      "training-" + this.state.trainingId + "name"
    );
    const newExerciseNumberInput = document.getElementById(
      "training-" + this.state.trainingId + "exercises"
    );
    if (newNameInput === "" || newExerciseNumberInput <= 0) {
      this.setState({
        trainingId: this.state.trainingId,
        name: this.state.name,
        exerciseNumber: this.state.exerciseNumber,
        public: this.state.public,
        loading: false,
        editValidateMessage: "Błędnie wprowadzone dane w formularzu",
        info: "",
      });
    }

    const response = await fetch("/api/trainings/" + this.state.trainingId, {
      method: "PUT",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        trainingId: this.state.trainingId,
        name: newNameInput.value,
        exerciseNumber: parseInt(newExerciseNumberInput.value),
        public: this.state.public,
      }),
    });
    if (response.ok) {
      this.setState({
        trainingId: this.state.trainingId,
        name: newNameInput.value,
        exerciseNumber: newExerciseNumberInput.value,
        public: this.state.public,
        loading: false,
        editValidateMessage: "",
        info: "Zmiany zostały zapisane",
      });
    }
  };
  renderInfo = () => {
    if (this.state.info !== "") {
      return (
        <div className="alert alert-success" role="alert">
          <div className="float-left">{this.state.info}</div>
          <div className="float-right">
            <i className="icon-cancel" onClick={this.handleCloseMessege}></i>
          </div>
          <div style={{ clear: "both" }}></div>
        </div>
      );
    }
    return null;
  };
  renderValidateMessage = () => {
    if (this.state.editValidateMessage !== "") {
      return (
        <div className="alert alert-danger" role="alert">
          {this.state.editValidateMessage}
        </div>
      );
    }
    return null;
  };
  async fetchTrainingData() {
    const response = await fetch("/api/trainings/" + this.state.trainingId, {
      method: "GET",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
    });
    const data = await response.json();
    this.setState({
      trainingId: this.state.trainingId,
      name: data.name,
      exerciseNumber: data.exerciseNumber,
      public: data.public,
      loading: false,
      editValidateMessage: "",
      info: "",
    });
  }
  renderBody() {
    if (this.state.loading) return <p>Trwa ładowanie...</p>;
    return (
      <React.Fragment>
        {this.renderInfo()}
        <div className="py-1 font-size-large letter-spacing-1">
          {/* detail view button and training name */}
          <div className="float-left">
            <a
              className="link-black"
              data-toggle="collapse"
              href={"#training-" + this.state.trainingId}
              role="button"
              aria-expanded="false"
              aria-controls="collapseExample"
            >
              <i className="icon-down-dir"></i>
              <span className="">{this.state.name}</span>
            </a>
          </div>
          {/* edit button */}
          <div className="float-right">
            <a
              href={"#training-edit-" + this.state.trainingId}
              className="link-black"
              data-toggle="collapse"
              role="button"
              aria-expanded="false"
              aria-controls="collapseExample"
            >
              <i className="icon-edit"></i>
            </a>
          </div>
          <div style={{ clear: "both" }}></div>
        </div>
        {/* collapsed edit fields */}
        <div className="collapse" id={"training-edit-" + this.state.trainingId}>
          <div>
            <label
              className="mt-1"
              htmlFor={"training-" + this.state.trainingId + "name"}
            >
              Nazwa
            </label>
            <input
              className="form-control"
              type="text"
              id={"training-" + this.state.trainingId + "name"}
              defaultValue={this.state.name}
            ></input>
            <label
              className="mt-1"
              htmlFor={"training-" + this.state.trainingId + "exercises"}
            >
              Ilość ćwiczeń
            </label>
            <input
              className="form-control"
              type="number"
              id={"training-" + this.state.trainingId + "exercises"}
              defaultValue={this.state.exerciseNumber}
              min="1"
              max="10"
            ></input>
            {this.renderValidateMessage()}
            <button
              className="btn btn-outline-primary mt-2"
              onClick={this.handleEdit}
            >
              Zatwierdź
            </button>
          </div>
        </div>
        {/* collapsed trainig detail */}
        <div className="collapse" id={"training-" + this.state.trainingId}>
          <div className="card card-body">
            Anim pariatur cliche reprehenderit, enim eiusmod high life accusamus
            terry richardson ad squid. Nihil anim keffiyeh helvetica, craft beer
            labore wes anderson cred nesciunt sapiente ea proident.
          </div>
        </div>
      </React.Fragment>
    );
  }
  render() {
    return this.renderBody();
  }
}