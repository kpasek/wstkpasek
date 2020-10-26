import React, { Component } from "react";

export class SExercise extends Component {
  static displayName = SExercise.name;

  constructor(props) {
    super(props);
    this.state = { exercise: this.props.exercise, series: [], loading: false };
  }

  async componentDidMount() {
    await this.fetchData();
  }
  fetchData = async () => {
    const seriesRespone = await fetch(
      "api/schedule/series/e/" + this.state.exercise.scheduleExerciseId,
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
    const series = await seriesRespone.json();
    this.setState({
      series: series,
      loading: false,
    });
  };
  handleDeleteSeries = async (seriesId) => {
    await fetch("api/schedule/series/" + seriesId, {
      method: "DELETE",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
    });
    this.setState({
      series: this.state.series.filter((s) => s.scheduleSeriesId !== seriesId),
    });
  };
  handleAdd = async (seriesId, fieldType) => {
    let input = document.getElementById("series-" + fieldType + "-" + seriesId);
    input.value = parseInt(input.value) + 1;
    const editedSeries = this.state.series.find(
      (element) => element.scheduleSeriesId === seriesId
    );
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
    const putResult = await fetch("api/schedule/series/" + seriesId, {
      method: "PUT",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        scheduleExerciseId: this.state.exercise.scheduleExerciseId,
        scheduleSeriesId: seriesId,
        name: "",
        order: editedSeries.order,
        repeats: editedSeries.repeats,
        load: editedSeries.load,
        distance: editedSeries.distance,
        time: editedSeries.time,
        restTime: editedSeries.restTime,
      }),
    });
    const newSeries = await putResult.json();
    let series2 = this.state.series.filter(
      (s) => s.scheduleSeriesId !== seriesId
    );
    series2.push(newSeries);
    series2.sort((a, b) => {
      return a.order >= b.order ? 1 : -1;
    });
    this.setState({
      series: series2,
    });
  };
  handleMinus = async (seriesId, fieldType) => {
    let input = document.getElementById("series-" + fieldType + "-" + seriesId);
    input.value = parseInt(input.value) - 1;
    const editedSeries = this.state.series.find(
      (element) => element.scheduleSeriesId === seriesId
    );
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
    const putResult = await fetch("api/schedule/series/" + seriesId, {
      method: "PUT",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        scheduleExerciseId: this.state.exercise.scheduleExerciseId,
        scheduleSeriesId: seriesId,
        name: "",
        order: editedSeries.order,
        repeats: editedSeries.repeats,
        load: editedSeries.load,
        distance: editedSeries.distance,
        time: editedSeries.time,
        restTime: editedSeries.restTime,
      }),
    });
    const newSeries = await putResult.json();
    let series = this.state.series.filter(
      (s) => s.scheduleSeriesId !== seriesId
    );
    series.push(newSeries);
    series.sort((a, b) => {
      return a.order >= b.order ? 1 : -1;
    });
    this.setState({
      series: series,
    });
  };
  handleAddSeries = async () => {
    const seriesResponse = await fetch("api/schedule/series", {
      method: "POST",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        scheduleExerciseId: this.state.exercise.scheduleExerciseId,
      }),
    });
    const series = await seriesResponse.json();
    this.state.series.push(series);
    this.setState({
      series: this.state.series,
    });
  };
  handleChangeSeries = async (seriesId) => {
    let inputRepeats = document.getElementById("series-reapets-" + seriesId);
    let inputLoad = document.getElementById("series-load-" + seriesId);
    let inputDistance = document.getElementById("series-distance-" + seriesId);
    let inputTime = document.getElementById("series-time-" + seriesId);
    let inputRestTime = document.getElementById("series-restTime-" + seriesId);

    const editedSeries = this.state.series.find(
      (element) => element.scheduleSeriesId === seriesId
    );
    editedSeries.repeats =
      inputRepeats.value === "" ? 0 : parseInt(inputRepeats.value);
    editedSeries.load = inputLoad.value === "" ? 0 : parseInt(inputLoad.value);
    editedSeries.distance =
      inputDistance.value === "" ? 0 : parseInt(inputDistance.value);
    editedSeries.time = inputTime.value === "" ? 0 : parseInt(inputTime.value);
    editedSeries.restTime =
      inputRestTime.value === "" ? 0 : parseInt(inputRestTime.value);
    const putResult = await fetch("api/schedule/series/" + seriesId, {
      method: "PUT",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        scheduleExerciseId: this.state.exercise.scheduleExerciseId,
        scheduleSeriesId: seriesId,
        name: "",
        order: editedSeries.order,
        repeats: editedSeries.repeats,
        load: editedSeries.load,
        distance: editedSeries.distance,
        time: editedSeries.time,
        restTime: editedSeries.restTime,
      }),
    });
    const newSeries = await putResult.json();
    let series = this.state.series.filter(
      (s) => s.scheduleSeriesId !== seriesId
    );
    series.push(newSeries);
    series.sort((a, b) => {
      return a.order >= b.order ? 1 : -1;
    });
    this.setState({
      series: series,
    });
  };
  renderSeries() {
    if (this.state.loading) {
      return <div>trwa ładowanie</div>;
    } else {
      return (
        <React.Fragment>
          {this.state.series.map((s) => (
            <div key={s.scheduleSeriesId} className="font-size-18">
              <div className="">
                <i
                  className="icon-trash-empty color-red font-size-20"
                  href={"#s-exercise-series-" + s.scheduleSeriesId}
                  type="button"
                  data-toggle="collapse"
                  data-target={"#s-exercise-series-" + s.scheduleSeriesId}
                  aria-expanded="false"
                  aria-controls={"#s-exercise-series-" + s.scheduleSeriesId}
                />
                <a
                  className="link-black"
                  data-toggle="collapse"
                  data-target={"#series-" + s.scheduleSeriesId}
                  href={"#series-" + s.scheduleSeriesId}
                  role="button"
                  aria-expanded="false"
                  aria-controls="collapseSeriesEdit"
                >
                  {s.order + " - " + s.name}
                </a>
              </div>
              {/* collapsed delete series */}
              <div
                className="collapse"
                id={"s-exercise-series-" + s.scheduleSeriesId}
              >
                <button
                  className="btn btn-outline-danger my-2"
                  onClick={() => {
                    this.handleDeleteSeries(s.scheduleSeriesId);
                  }}
                >
                  Usuń serie
                </button>
              </div>
              <div
                className="collapse mb-2 col-lg-6"
                id={"series-" + s.scheduleSeriesId}
              >
                {/* repeats input */}
                <div className="row mx-auto mt-1">
                  <label
                    htmlFor={"series-reapets-" + s.scheduleSeriesId}
                    className="col-12 font-size-18"
                  >
                    Powtórzenia
                  </label>
                  <button
                    className="btn btn-outline-secondary col-2"
                    onClick={() => {
                      this.handleAdd(s.scheduleSeriesId, "reapets");
                    }}
                  >
                    <i className="icon-plus"></i>
                  </button>
                  <input
                    className="form-control col-7 col-lg-6 text-center mx-1"
                    id={"series-reapets-" + s.scheduleSeriesId}
                    type="number"
                    min="0"
                    defaultValue={s.repeats}
                    onChangeCapture={() => {
                      this.handleChangeSeries(s.scheduleSeriesId);
                    }}
                  />

                  <button
                    className="btn btn-outline-secondary col-2"
                    onClick={() => {
                      this.handleMinus(s.scheduleSeriesId, "reapets");
                    }}
                  >
                    <i className="icon-minus"></i>
                  </button>
                </div>
                {/* load input */}
                <div className="row mx-auto mt-1">
                  <label
                    htmlFor={"series-load-" + s.scheduleSeriesId}
                    className="col-12 font-size-18"
                  >
                    Obciążenie
                  </label>
                  <button
                    className="btn btn-outline-secondary col-2"
                    onClick={() => {
                      this.handleAdd(s.scheduleSeriesId, "load");
                    }}
                  >
                    <i className="icon-plus"></i>
                  </button>
                  <input
                    className="form-control col-7 col-lg-6 text-center mx-1"
                    id={"series-load-" + s.scheduleSeriesId}
                    type="number"
                    min="0"
                    defaultValue={s.load}
                    onChangeCapture={() => {
                      this.handleChangeSeries(s.scheduleSeriesId);
                    }}
                  />

                  <button
                    className="btn btn-outline-secondary col-2"
                    onClick={() => {
                      this.handleMinus(s.scheduleSeriesId, "load");
                    }}
                  >
                    <i className="icon-minus"></i>
                  </button>
                </div>
                {/* distance input */}
                <div className="row mx-auto mt-1">
                  <label
                    htmlFor={"series-distance-" + s.scheduleSeriesId}
                    className="col-12 font-size-18"
                  >
                    Dystans
                  </label>
                  <button
                    className="btn btn-outline-secondary col-2"
                    onClick={() => {
                      this.handleAdd(s.scheduleSeriesId, "distance");
                    }}
                  >
                    <i className="icon-plus"></i>
                  </button>
                  <input
                    className="form-control col-7 col-lg-6 text-center mx-1"
                    id={"series-distance-" + s.scheduleSeriesId}
                    type="number"
                    min="0"
                    defaultValue={s.distance}
                    onChangeCapture={() => {
                      this.handleChangeSeries(s.scheduleSeriesId);
                    }}
                  />

                  <button
                    className="btn btn-outline-secondary col-2"
                    onClick={() => {
                      this.handleMinus(s.scheduleSeriesId, "distance");
                    }}
                  >
                    <i className="icon-minus"></i>
                  </button>
                </div>
                {/* time input */}
                <div className="row mx-auto mt-1">
                  <label
                    htmlFor={"series-time-" + s.scheduleSeriesId}
                    className="col-12 font-size-18"
                  >
                    Czas
                  </label>
                  <button
                    className="btn btn-outline-secondary col-2"
                    onClick={() => {
                      this.handleAdd(s.scheduleSeriesId, "time");
                    }}
                  >
                    <i className="icon-plus"></i>
                  </button>
                  <input
                    className="form-control col-7 col-lg-6 text-center mx-1"
                    id={"series-time-" + s.scheduleSeriesId}
                    type="number"
                    min="0"
                    defaultValue={s.time}
                    onChangeCapture={() => {
                      this.handleChangeSeries(s.scheduleSeriesId);
                    }}
                  />

                  <button
                    className="btn btn-outline-secondary col-2"
                    onClick={() => {
                      this.handleMinus(s.scheduleSeriesId, "time");
                    }}
                  >
                    <i className="icon-minus"></i>
                  </button>
                </div>
                {/* rest time input */}
                <div className="row mx-auto mt-1">
                  <label
                    htmlFor={"series-restTime-" + s.scheduleSeriesId}
                    className="col-12 font-size-18"
                  >
                    Odpoczynek
                  </label>
                  <button
                    className="btn btn-outline-secondary col-2"
                    onClick={() => {
                      this.handleAdd(s.scheduleSeriesId, "restTime");
                    }}
                  >
                    <i className="icon-plus"></i>
                  </button>
                  <input
                    className="form-control col-7 col-lg-6 text-center mx-1"
                    id={"series-restTime-" + s.scheduleSeriesId}
                    type="number"
                    min="0"
                    defaultValue={s.restTime}
                    onChangeCapture={() => {
                      this.handleChangeSeries(s.scheduleSeriesId);
                    }}
                  />

                  <button
                    className="btn btn-outline-secondary col-2"
                    onClick={() => {
                      this.handleMinus(s.scheduleSeriesId, "restTime");
                    }}
                  >
                    <i className="icon-minus"></i>
                  </button>
                </div>
              </div>
            </div>
          ))}
        </React.Fragment>
      );
    }
  }
  renderBody() {
    return (
      <React.Fragment>
        <span>
          {/* exercise name */}
          <div className="float-left">
            <a
              href={"#s-exercise-" + this.state.exercise.scheduleExerciseId}
              className="font-size-20 mt-2 max-width-exercise-list link-black"
              type="button"
              data-toggle="collapse"
              data-target={
                "#s-exercise-" + this.state.exercise.scheduleExerciseId
              }
              aria-expanded="false"
              aria-controls={
                "s-exercise-" + this.state.exercise.scheduleExerciseId
              }
            >
              {this.state.exercise.exercise.name}
            </a>
            {/* exercise part */}
            <div className="font-weight-300 mt-1">
              {this.state.exercise.exercise.partId}
            </div>
          </div>
          <div className="float-right py-2">
            <i
              className="icon-edit font-size-large"
              type="button"
              data-toggle="collapse"
              data-target={
                "#edit-exercise-" + this.state.exercise.scheduleExerciseId
              }
              aria-expanded="false"
              aria-controls={
                "edit-exercise-" + this.state.exercise.scheduleExerciseId
              }
            />
            <i
              className="icon-trash-empty color-red font-size-large ml-1 pt-3"
              type="button"
              data-toggle="collapse"
              data-target={
                "#delete-exercise-" + this.state.exercise.scheduleExerciseId
              }
              aria-expanded="false"
              aria-controls={
                "delete-exercise-" + this.state.exercise.scheduleExerciseId
              }
              // onClick={() => {
              //   this.props.onDelete(this.state.exercise.scheduleExerciseId);
              // }}
            />
          </div>
          <div style={{ clear: "both" }}></div>
        </span>
        {/* edit fields */}
        <span>
          {/* edit exercise fields */}
          <div
            id={"edit-exercise-" + this.state.exercise.scheduleExerciseId}
            className="collapse"
            aria-labelledby="editExercise"
          >
            <div className="">
              <h4>Edytuj ćwiczenie</h4>
              <label
                htmlFor={
                  "edit-exercise-name-" + this.state.exercise.scheduleExerciseId
                }
              >
                Nazwa
              </label>
              <input
                className="form-control"
                type="text"
                required
                id={
                  "edit-exercise-name-" + this.state.exercise.scheduleExerciseId
                }
                defaultValue={this.state.exercise.exercise.name}
              />

              <label
                htmlFor={
                  "edit-exercise-part-" + this.state.exercise.scheduleExerciseId
                }
              >
                Partia
              </label>
              <input
                className="form-control"
                type="text"
                list="part-list"
                id={
                  "edit-exercise-part-" + this.state.exercise.scheduleExerciseId
                }
                defaultValue={this.state.exercise.exercise.partId}
              />
              <label
                htmlFor={
                  "edit-exercise-type-" + this.state.exercise.scheduleExerciseId
                }
              >
                Typ
              </label>
              <input
                className="form-control"
                type="text"
                list="type-list"
                id={
                  "edit-exercise-type-" + this.state.exercise.scheduleExerciseId
                }
                defaultValue={this.state.exercise.exercise.typeId}
              />
              <label
                htmlFor={
                  "edit-exercise-descriprion-" +
                  this.state.exercise.scheduleExerciseId
                }
              >
                Opis
              </label>
              <input
                className="form-control"
                type="text"
                id={
                  "edit-exercise-description-" +
                  this.state.exercise.scheduleExerciseId
                }
                defaultValue={this.state.exercise.exercise.description}
              />
              <label
                htmlFor={
                  "edit-exercise-order-" +
                  this.state.exercise.scheduleExerciseId
                }
              >
                Kolejność
              </label>
              <input
                className="form-control"
                type="number"
                min="1"
                id={
                  "edit-exercise-order-" +
                  this.state.exercise.scheduleExerciseId
                }
                defaultValue={this.state.exercise.order}
              />
              <button
                className="btn btn-outline-primary my-2"
                onClick={() => {
                  this.props.onEdit(
                    this.state.exercise.exercise.exerciseId,
                    this.state.exercise.scheduleExerciseId
                  );
                }}
              >
                Zatwierdź
              </button>
            </div>
          </div>
        </span>
        {/* series */}
        <span>
          <div
            className="collapse ml-1"
            id={"s-exercise-" + this.state.exercise.scheduleExerciseId}
          >
            {this.renderSeries()}
            <button
              className="btn btn-outline-success my-2"
              onClick={this.handleAddSeries}
            >
              Nowa seria
            </button>
          </div>
        </span>
      </React.Fragment>
    );
  }
  render() {
    return this.renderBody();
  }
}
