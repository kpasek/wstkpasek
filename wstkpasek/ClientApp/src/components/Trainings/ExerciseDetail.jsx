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
      series: [],
      loading: true,
    };
  }
  componentDidMount = async () => {
    await this.fetchData();
  };
  changeSeriesOrder = async (seriesId) => {
    let input = document.getElementById("series-order-" + seriesId);
    await fetch("api/series/order", {
      method: "POST",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        seriesId: seriesId,
        order: parseInt(input.value),
      }),
    });
    const seriesResponse = await fetch(
      "api/series/exercise/" + this.state.exerciseId,
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
    const newSeries = await seriesResponse.json();
    this.setState({
      exerciseId: this.state.exerciseId,
      name: this.state.name,
      partId: this.state.partId,
      order: this.state.order,
      typeId: this.state.typeId,
      description: this.state.description,
      public: this.state.public,
      series: newSeries,
      parts: this.state.parts,
      types: this.state.types,

      loading: false,
    });
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

    const seriesResponse = await fetch(
      "api/series/exercise/" + this.state.exerciseId,
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

    const series = await seriesResponse.json();

    this.setState({
      exerciseId: this.props.exerciseId,
      name: data.name,
      partId: data.partId,
      order: order,
      typeId: data.typeId,
      description: data.description,
      public: data.public,
      series: series,
      parts: this.state.parts,
      types: this.state.types,

      loading: false,
    });
  }
  handleDeleteSeries = async (seriesId) => {
    await fetch("api/series/" + seriesId, {
      method: "DELETE",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
    });
    const index = this.state.series.indexOf(
      (item) => item.seriesId === seriesId
    );
    this.state.series.splice(index, 1);
    this.setState({
      exerciseId: this.state.exerciseId,
      name: this.state.name,
      partId: this.state.partId,
      order: this.state.order,
      typeId: this.state.typeId,
      description: this.state.description,
      public: this.state.public,
      series: this.state.series,
      parts: this.state.parts,
      types: this.state.types,

      loading: this.state.loading,
    });
  };
  handleNewSeries = async () => {
    const postResult = await fetch("api/series", {
      method: "POST",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        exerciseId: this.state.exerciseId,
        name: "nowa",
        repeats: 0,
        load: 0,
        distance: 0,
        time: 0,
        restTime: 0,
      }),
    });
    const newSeries = await postResult.json();
    this.state.series.push(newSeries);
    this.setState({
      exerciseId: this.state.exerciseId,
      name: this.state.name,
      partId: this.state.partId,
      order: this.state.order,
      typeId: this.state.typeId,
      description: this.state.description,
      public: this.state.public,
      series: this.state.series,
      parts: this.state.parts,
      types: this.state.types,

      loading: this.state.loading,
    });
  };
  handleMinus = async (seriesId, fieldType) => {
    let input = document.getElementById("series-" + fieldType + "-" + seriesId);
    input.value = parseInt(input.value) - 1;
    const editedSeries = this.state.series.find(
      (element) => element.seriesId === seriesId
    );
    const seriesIndex = this.state.series.indexOf(editedSeries);
    switch (fieldType) {
      case "reapets":
        editedSeries.repeats--;
        break;
      case "load":
        editedSeries.load--;
        break;
      case "distance":
        editedSeries.distance--;
        break;
      case "time":
        editedSeries.time--;
        break;
      case "restTime":
        editedSeries.restTime--;
        break;
      default:
    }
    await this.saveChanges(seriesId, editedSeries, seriesIndex);
  };
  handleChangeSeries = async (seriesId) => {
    let inputRepeats = document.getElementById("series-reapets-" + seriesId);
    let inputLoad = document.getElementById("series-load-" + seriesId);
    let inputDistance = document.getElementById("series-distance-" + seriesId);
    let inputTime = document.getElementById("series-time-" + seriesId);
    let inputRestTime = document.getElementById("series-restTime-" + seriesId);

    const editedSeries = this.state.series.find(
      (element) => element.seriesId === seriesId
    );
    const seriesIndex = this.state.series.indexOf(editedSeries);
    editedSeries.repeats =
      inputRepeats.value === "" ? 0 : parseInt(inputRepeats.value);
    editedSeries.load = inputLoad.value === "" ? 0 : parseInt(inputLoad.value);
    editedSeries.distance =
      inputDistance.value === "" ? 0 : parseInt(inputDistance.value);
    editedSeries.time = inputTime.value === "" ? 0 : parseInt(inputTime.value);
    editedSeries.restTime =
      inputRestTime.value === "" ? 0 : parseInt(inputRestTime.value);
    await this.saveChanges(seriesId, editedSeries, seriesIndex);
  };

  handleAdd = async (seriesId, fieldType) => {
    let input = document.getElementById("series-" + fieldType + "-" + seriesId);
    input.value = parseInt(input.value) + 1;
    const editedSeries = this.state.series.find(
      (element) => element.seriesId === seriesId
    );
    const seriesIndex = this.state.series.indexOf(editedSeries);
    switch (fieldType) {
      case "reapets":
        editedSeries.repeats++;
        break;
      case "load":
        editedSeries.load++;
        break;
      case "distance":
        editedSeries.distance++;
        break;
      case "time":
        editedSeries.time++;
        break;
      case "restTime":
        editedSeries.restTime++;
        break;
      default:
    }
    await this.saveChanges(seriesId, editedSeries, seriesIndex);
  };
  async saveChanges(seriesId, editedSeries, seriesIndex) {
    const putResult = await fetch("api/series/" + seriesId, {
      method: "PUT",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        exerciseId: this.state.exerciseId,
        seriesId: seriesId,
        name: "",
        order: this.state.order,
        repeats: editedSeries.repeats,
        load: editedSeries.load,
        distance: editedSeries.distance,
        time: editedSeries.time,
        restTime: editedSeries.restTime,
      }),
    });

    const newSeries = await putResult.json();
    this.state.series[seriesIndex] = newSeries;
    this.setState({
      exerciseId: this.state.exerciseId,
      name: this.state.name,
      partId: this.state.partId,
      order: this.state.order,
      typeId: this.state.typeId,
      description: this.state.description,
      public: this.state.public,
      series: this.state.series,
      parts: this.state.parts,
      types: this.state.types,

      loading: this.state.loading,
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
      series: this.state.series,
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
                href={"#series-exercise-" + this.state.exerciseId}
                className="font-size-20 mt-2 max-width-exercise-list link-black"
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
                <div className="">
                  {this.state.series.map((s) => (
                    <div key={s.seriesId} className="ml-1">
                      <div className="font-size-18">
                        <i
                          data-toggle="collapse"
                          href={"#series-delete-" + s.seriesId}
                          role="button"
                          aria-expanded="false"
                          aria-controls="deleteSeriesCollapse"
                          className="icon-trash-empty font-size-large color-red"
                        ></i>
                        <a
                          className="link-black"
                          data-toggle="collapse"
                          href={"#series-" + s.seriesId}
                          role="button"
                          aria-expanded="false"
                          aria-controls="seriesCollapse"
                        >
                          {s.order + " - " + s.name}
                        </a>
                      </div>
                      <div
                        className="collapse"
                        id={"series-delete-" + s.seriesId}
                      >
                        <button
                          className="btn btn-outline-danger"
                          onClick={() => {
                            this.handleDeleteSeries(s.seriesId);
                          }}
                        >
                          Usuń
                        </button>
                      </div>
                      <div
                        className="collapse mx-auto mb-2"
                        id={"series-" + s.seriesId}
                      >
                        {/* repeats input */}
                        <div className="row mx-auto mt-1">
                          <label
                            htmlFor={"series-reapets-" + s.seriesId}
                            className="col-12 font-size-18"
                          >
                            Powtórzenia
                          </label>
                          <button
                            className="btn btn-outline-secondary col-2"
                            onClick={() => {
                              this.handleAdd(s.seriesId, "reapets");
                            }}
                          >
                            <i className="icon-plus"></i>
                          </button>
                          <input
                            className="form-control col-7 col-lg-6 text-center mx-1"
                            id={"series-reapets-" + s.seriesId}
                            type="number"
                            min="0"
                            defaultValue={s.repeats}
                            onChangeCapture={() => {
                              this.handleChangeSeries(s.seriesId);
                            }}
                          />

                          <button
                            className="btn btn-outline-secondary col-2"
                            onClick={() => {
                              this.handleMinus(s.seriesId, "reapets");
                            }}
                          >
                            <i className="icon-minus"></i>
                          </button>
                        </div>
                        {/* load input */}
                        <div className="row mx-auto mt-1">
                          <label
                            htmlFor={"series-load-" + s.seriesId}
                            className="col-12 font-size-18"
                          >
                            Obciążenie
                          </label>
                          <button
                            className="btn btn-outline-secondary col-2"
                            onClick={() => {
                              this.handleAdd(s.seriesId, "load");
                            }}
                          >
                            <i className="icon-plus"></i>
                          </button>
                          <input
                            className="form-control col-7 col-lg-6 text-center mx-1"
                            id={"series-load-" + s.seriesId}
                            type="number"
                            min="0"
                            defaultValue={s.load}
                            onChangeCapture={() => {
                              this.handleChangeSeries(s.seriesId);
                            }}
                          />

                          <button
                            className="btn btn-outline-secondary col-2"
                            onClick={() => {
                              this.handleMinus(s.seriesId, "load");
                            }}
                          >
                            <i className="icon-minus"></i>
                          </button>
                        </div>
                        {/* distance input */}
                        <div className="row mx-auto mt-1">
                          <label
                            htmlFor={"series-distance-" + s.seriesId}
                            className="col-12 font-size-18"
                          >
                            Dystans
                          </label>
                          <button
                            className="btn btn-outline-secondary col-2"
                            onClick={() => {
                              this.handleAdd(s.seriesId, "distance");
                            }}
                          >
                            <i className="icon-plus"></i>
                          </button>
                          <input
                            className="form-control col-7 col-lg-6 text-center mx-1"
                            id={"series-distance-" + s.seriesId}
                            type="number"
                            min="0"
                            defaultValue={s.distance}
                            onChangeCapture={() => {
                              this.handleChangeSeries(s.seriesId);
                            }}
                          />

                          <button
                            className="btn btn-outline-secondary col-2"
                            onClick={() => {
                              this.handleMinus(s.seriesId, "distance");
                            }}
                          >
                            <i className="icon-minus"></i>
                          </button>
                        </div>
                        {/* time input */}
                        <div className="row mx-auto mt-1">
                          <label
                            htmlFor={"series-time-" + s.seriesId}
                            className="col-12 font-size-18"
                          >
                            Czas
                          </label>
                          <button
                            className="btn btn-outline-secondary col-2"
                            onClick={() => {
                              this.handleAdd(s.seriesId, "time");
                            }}
                          >
                            <i className="icon-plus"></i>
                          </button>
                          <input
                            className="form-control col-7 col-lg-6 text-center mx-1"
                            id={"series-time-" + s.seriesId}
                            type="number"
                            min="0"
                            defaultValue={s.time}
                            onChangeCapture={() => {
                              this.handleChangeSeries(s.seriesId);
                            }}
                          />

                          <button
                            className="btn btn-outline-secondary col-2"
                            onClick={() => {
                              this.handleMinus(s.seriesId, "time");
                            }}
                          >
                            <i className="icon-minus"></i>
                          </button>
                        </div>
                        {/* rest time input */}
                        <div className="row mx-auto mt-1">
                          <label
                            htmlFor={"series-restTime-" + s.seriesId}
                            className="col-12 font-size-18"
                          >
                            Odpoczynek
                          </label>
                          <button
                            className="btn btn-outline-secondary col-2"
                            onClick={() => {
                              this.handleAdd(s.seriesId, "restTime");
                            }}
                          >
                            <i className="icon-plus"></i>
                          </button>
                          <input
                            className="form-control col-7 col-lg-6 text-center mx-1"
                            id={"series-restTime-" + s.seriesId}
                            type="number"
                            min="0"
                            defaultValue={s.restTime}
                            onChangeCapture={() => {
                              this.handleChangeSeries(s.seriesId);
                            }}
                          />

                          <button
                            className="btn btn-outline-secondary col-2"
                            onClick={() => {
                              this.handleMinus(s.seriesId, "restTime");
                            }}
                          >
                            <i className="icon-minus"></i>
                          </button>
                        </div>
                        {/* order input */}
                        <div className="row mx-auto mt-1">
                          <label
                            htmlFor={"series-order-" + s.seriesId}
                            className="col-12 font-size-18"
                          >
                            Kolejność
                          </label>
                          <input
                            className="form-control col-9 col-lg-6 text-center mx-1"
                            id={"series-order-" + s.seriesId}
                            type="number"
                            min="1"
                            defaultValue={s.order}
                          />

                          <button
                            className="btn btn-outline-secondary col-2"
                            onClick={() => {
                              this.changeSeriesOrder(s.seriesId);
                            }}
                          >
                            Zatwierdź
                          </button>
                        </div>
                      </div>
                    </div>
                  ))}
                  <button
                    className="ml-2 mt-2 cursor-pointer btn btn-outline-primary"
                    onClick={this.handleNewSeries}
                  >
                    Dodaj
                  </button>
                </div>
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
