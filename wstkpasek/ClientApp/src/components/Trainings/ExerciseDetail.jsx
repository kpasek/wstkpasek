import React, { Component } from "react";

export default class ExerciseDetail extends Component {
  constructor(props) {
    super(props);

    this.state = {
      exerciseId: this.props.exerciseId,
      name: "",
      partId: "",
      order: 0,
      typeId: "",
      description: "",
      public: false,
      parts: this.props.parts,
      types: this.props.types,

      loading: true,
    };
  }
  componentDidMount = async () => {
    await this.fetchData();
  };

  async fetchData() {
    const response = await fetch("/api/exercises/" + this.state.exerciseId, {
      method: "GET",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
    });
    const data = await response.json();

    const orderResponse = await fetch("api/exercises/order", {
      method: "POST",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        trainingId: this.props.trainingId,
        exerciseId: this.state.exerciseId,
      }),
    });

    const order = await orderResponse.json();
    this.setState({
      exerciseId: this.props.exerciseId,
      name: data.name,
      partId: data.partId,
      order: order,
      typeId: data.typeId,
      description: data.description,
      public: data.public,
      parts: this.state.parts,
      types: this.state.types,

      loading: false,
    });
  }

  handleEditExercise = async () => {
    const newName = document.getElementById(
      "edit-exercise-name-" + this.state.exerciseId
    ).value;
    const newPart = document.getElementById(
      "edit-exercise-part-" + this.state.exerciseId
    ).value;
    const newType = document.getElementById(
      "edit-exercise-type-" + this.state.exerciseId
    ).value;
    const newDescription = document.getElementById(
      "edit-exercise-description-" + this.state.exerciseId
    ).value;
    const newOrder = document.getElementById(
      "edit-exercise-order-" + this.state.exerciseId
    ).value;

    const updateTask = fetch("api/exercises/" + this.state.exerciseId, {
      method: "PUT",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        exerciseId: this.state.exerciseId,
        name: newName,
        partId: newPart,
        typeId: newType,
        description: newDescription,
        public: this.state.public,
      }),
    });

    const changeOrderTask = fetch("api/exercises/change-order", {
      method: "POST",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        trainingId: this.props.trainingId,
        exerciseId: this.state.exerciseId,
        order: parseInt(newOrder),
      }),
    });
    await updateTask;
    await changeOrderTask;

    this.setState({
      exerciseId: this.props.exerciseId,
      name: newName,
      partId: newPart,
      order: parseInt(newOrder),
      typeId: newType,
      description: newDescription,
      public: this.state.public,
      parts: this.state.parts,
      types: this.state.types,

      loading: false,
    });
    await this.props.refresh();
  };
  renderBody = () => {
    if (this.state.loading) {
      return <h2>Trwa ładowanie</h2>;
    } else {
      return (
        <React.Fragment>
          <span
            key={"detail-exercise-" + this.state.exerciseId}
            className="ml-1"
          >
            <div className="float-left">
              {/* exercise name */}
              <a
                className="font-size-20 mt-2 max-width-exercise-list"
                type="button"
                data-toggle="collapse"
                data-target={"#series-exercise-" + this.state.exerciseId}
                aria-expanded="false"
                aria-controls={"series-exercise-" + this.state.exerciseId}
              >
                {this.state.name}
              </a>
              {/* exercise part */}
              <div className="font-weight-300 mt-1">{this.state.partId}</div>
            </div>
            <div className="float-right py-1">
              {/* exit button */}
              <i
                className="icon-edit font-size-large"
                type="button"
                data-toggle="collapse"
                data-target={"#edit-exercise-" + this.state.exerciseId}
                aria-expanded="false"
                aria-controls={"edit-exercise-" + this.state.exerciseId}
              />
              {/* change button */}
              <i
                className="icon-loop font-size-large mx-1"
                type="button"
                data-toggle="collapse"
                data-target={"#change-exercise-" + this.state.exerciseId}
                aria-expanded="false"
                aria-controls={"change-exercise-" + this.state.exerciseId}
              />
              {/* delete button */}
              <i
                className="icon-trash-empty color-red font-size-large"
                type="button"
                data-toggle="collapse"
                data-target={"#delete-exercise-" + this.state.exerciseId}
                aria-expanded="false"
                aria-controls={"delete-exercise-" + this.state.exerciseId}
              />
            </div>
            <div className="" style={{ clear: "both" }} />
            <div className="accordion" id={"accordion-training"}>
              {/* edit series */}
              <div
                id={"series-exercise-" + this.state.exerciseId}
                className="collapse"
                aria-labelledby="seriesExercise"
                data-parent={"#accordion-training"}
              >
                {/* change exercise form */}
                <div className="">series</div>
              </div>
              {/* edit exercise fields */}
              <div
                id={"edit-exercise-" + this.state.exerciseId}
                className="collapse"
                aria-labelledby="editExercise"
                data-parent={"#accordion-training"}
              >
                <div className="">
                  <label
                    htmlFor={"edit-exercise-name-" + this.state.exerciseId}
                  >
                    Nazwa
                  </label>
                  <input
                    className="form-control"
                    type="text"
                    required
                    id={"edit-exercise-name-" + this.state.exerciseId}
                    defaultValue={this.state.name}
                  />

                  <label
                    htmlFor={"edit-exercise-part-" + this.state.exerciseId}
                  >
                    Partia
                  </label>
                  <input
                    className="form-control"
                    type="text"
                    list="parts-list"
                    id={"edit-exercise-part-" + this.state.exerciseId}
                    defaultValue={this.state.partId}
                  />
                  <label
                    htmlFor={"edit-exercise-type-" + this.state.exerciseId}
                  >
                    Typ
                  </label>
                  <input
                    className="form-control"
                    type="text"
                    list="types-list"
                    id={"edit-exercise-type-" + this.state.exerciseId}
                    defaultValue={this.state.typeId}
                  />
                  <label
                    htmlFor={
                      "edit-exercise-descriprion-" + this.state.exerciseId
                    }
                  >
                    Opis
                  </label>
                  <input
                    className="form-control"
                    type="text"
                    id={"edit-exercise-description-" + this.state.exerciseId}
                    defaultValue={this.state.description}
                  />
                  <label
                    htmlFor={"edit-exercise-order-" + this.state.exerciseId}
                  >
                    Kolejność
                  </label>
                  <input
                    className="form-control"
                    type="number"
                    min="1"
                    id={"edit-exercise-order-" + this.state.exerciseId}
                    defaultValue={this.state.order}
                  />
                  <button
                    className="btn btn-outline-primary my-2"
                    onClick={this.handleEditExercise}
                  >
                    Zatwierdź
                  </button>
                </div>
              </div>
              <div
                id={"change-exercise-" + this.state.exerciseId}
                className="collapse"
                aria-labelledby="changeExercise"
                data-parent={"#accordion-training"}
              >
                {/* change exercise form */}
                <div className="">change</div>
              </div>
              <div
                id={"delete-exercise-" + this.state.exerciseId}
                className="collapse"
                aria-labelledby="deleteExercise"
                data-parent={"#accordion-training"}
              >
                {/* delete exercise form */}
                <div className="">delete</div>
              </div>
            </div>
          </span>
        </React.Fragment>
      );
    }
  };

  render() {
    return this.renderBody();
  }
}
