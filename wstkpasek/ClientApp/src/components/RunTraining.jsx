import React, { Component } from "react";

export class RunTraining extends Component {
  static displayName = RunTraining.name;

  constructor(props) {
    super(props);
    this.state = {
      scheduleTrainingId: this.props.match.params.trainingId,
      typePL: {
        repeats: "Powtórzenia",
        load: "Obciążenie",
        time: "Czas",
        distance: "Dystans",
        restTime: "Odpoczynek",
      },
      loading: true,
    };
  }

  async componentDidMount() {
    await this.fetchData();
  }
  async fetchData() {
    const response = await fetch(
      "api/schedule/trainings/start/" + this.state.scheduleTrainingId,
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
    const exercises = await exercisesResponse.json();
    const parts = await responseParts.json();
    const data = await response.json();
    this.setState({
      training: data.training,
      seriesCurrent: data.seriesList,
      seriesToComplete: data.seriesToComplete,
      seriesCompleted: data.seriesCompleted,
      nextTrainingDate: data.nextTrainingDate,
      exercises: exercises,
      parts: parts,
      loading: false,
    });
    console.log(this.state);
  }
  handleChangePart = async (scheduleExerciseId) => {
    const part = document.getElementById(
      "select-exercise-part-" + scheduleExerciseId
    ).value;

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
  handleChange = async (type, series, op) => {
    const input = document.getElementById(
      "input-series-" + type + "-" + series.scheduleSeriesId
    );
    if (op < 0 && parseInt(input.value) === 0) return null;
    input.value = parseInt(input.value) + op;
    series[type] = parseInt(input.value);
    const seriesResponse = await fetch(
      "api/schedule/series/" + series.scheduleSeriesId,
      {
        method: "PUT",
        mode: "cors",
        cache: "no-cache",
        credentials: "include",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          scheduleSeriesId: series.scheduleSeriesId,
          order: series.order,
          scheduleExerciseId: series.scheduleExerciseId,
          repeats: series.repeats,
          load: series.load,
          time: series.time,
          distance: series.distance,
          restTime: series.restTime,
          intensity: series.intensity,
          finish: series.finish,
        }),
      }
    );
    const seriesUpdated = await seriesResponse.json();
    series.name = seriesUpdated.name;
    this.setState({
      seriesCurrent: this.state.seriesCurrent,
    });
  };
  handleFinishSeries = async (series) => {
    const task = fetch("api/schedule/series/" + series.scheduleSeriesId, {
      method: "PUT",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        scheduleSeriesId: series.scheduleSeriesId,
        order: series.order,
        scheduleExerciseId: series.scheduleExerciseId,
        repeats: series.repeats,
        load: series.load,
        time: series.time,
        distance: series.distance,
        restTime: series.restTime,
        intensity: series.intensity,
        finish: true,
      }),
    });
    this.state.seriesCompleted.push(series);
    const newSeries = this.state.seriesCurrent.filter(
      (s) => s.scheduleSeriesId !== series.scheduleSeriesId
    );
    if (this.state.seriesToComplete.length > 0) {
      newSeries.push(this.state.seriesToComplete[0]);
      this.state.seriesToComplete = this.state.seriesToComplete.filter(
        (s) =>
          s.scheduleSeriesId !== this.state.seriesToComplete[0].scheduleSeriesId
      );
    }
    this.setState({
      seriesCurrent: newSeries,
      seriesCompleted: this.state.seriesCompleted,
      seriesToComplete: this.state.seriesToComplete,
    });
    await task;
  };
  handleDeleteSeries = async (series) => {
    await fetch("api/schedule/series/" + series.scheduleSeriesId, {
      method: "DELETE",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
    });
    this.state.seriesCurrent = this.state.seriesCurrent.filter(
      (s) => s.scheduleSeriesId !== series.scheduleSeriesId
    );
    this.state.seriesToComplete = this.state.seriesToComplete.filter(
      (s) => s.scheduleSeriesId !== series.scheduleSeriesId
    );
    this.setState({
      seriesCurrent: this.state.seriesCurrent,
      seriesToComplete: this.state.seriesToComplete,
    });
  };
  handleChangeExercise = async (scheduleExerciseId) => {
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
  handleFinishTraining = async () => {
    const nextTraining = document.getElementById("next-training-checkbox")
      .checked;
    const updateSeries = document.getElementById("update-series-checkbox")
      .checked;
    const nextTrainingDate = document.getElementById("next-training-date")
      .value;
    const finishDate = document.getElementById("finish-training-date").value;

    await fetch("api/schedule/trainings/finish", {
      method: "POST",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        scheduleTrainingId: parseInt(this.state.scheduleTrainingId),
        createNextTraining: nextTraining,
        updateSeries: updateSeries,
        nextTrainingDate: nextTrainingDate,
        finishDate: finishDate,
      }),
    });
    document.location.href = "/harmonogram";
  };
  renderSelectPart = (scheduleExerciseId) => {
    return (
      <select
        className="custom-select my-2"
        id={"select-exercise-part-" + scheduleExerciseId}
        onChange={() => this.handleChangePart(scheduleExerciseId)}
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
  renderHistory(series) {
    if (series.history.length > 0) {
      return (
        <React.Fragment>
          {series.history[0].scheduleExercise.scheduleTraining.trainingDate.substr(
            0,
            10
          )}
          {series.history.map((s) => (
            <div key={s.scheduleSeriesId}>{s.order + " - " + s.name}</div>
          ))}
        </React.Fragment>
      );
    }
  }
  renderInput(type, series, hidden) {
    if (series[type] > 0 && !hidden) {
      return (
        <React.Fragment>
          <div className="">
            <button
              className="btn btn-outline-secondary"
              onClick={() => {
                this.handleChange(type, series, 1);
              }}
            >
              <i className="icon-plus" />
            </button>
            <input
              className="form-control w-25 d-inline text-center font-size-20 border-0"
              type="number"
              id={"input-series-" + type + "-" + series.scheduleSeriesId}
              defaultValue={series[type]}
            />
            <button
              className="btn btn-outline-secondary d-inline"
              onClick={() => {
                this.handleChange(type, series, -1);
              }}
            >
              <i className="icon-minus" />
            </button>
            <label
              className="w-100 font-size-18 m-minus"
              htmlFor={"input-series-" + type + "-" + series.scheduleSeriesId}
            >
              {this.state.typePL[type]}
            </label>
          </div>
        </React.Fragment>
      );
    } else if (series[type] <= 0 && hidden) {
      return (
        <React.Fragment>
          <div className="">
            <button
              className="btn btn-outline-secondary"
              onClick={() => {
                this.handleChange(type, series, 1);
              }}
            >
              <i className="icon-plus" />
            </button>
            <input
              className="form-control w-25 d-inline text-center font-size-20 border-0"
              type="number"
              id={"input-series-" + type + "-" + series.scheduleSeriesId}
              defaultValue={series[type]}
            />
            <button
              className="btn btn-outline-secondary d-inline"
              onClick={() => {
                this.handleChange(type, series, -1);
              }}
            >
              <i className="icon-minus" />
            </button>
            <label
              className="w-100 font-size-18 m-minus"
              htmlFor={"input-series-" + type + "-" + series.scheduleSeriesId}
            >
              {this.state.typePL[type]}
            </label>
          </div>
        </React.Fragment>
      );
    }
  }
  renderCurrent() {
    if (this.state.seriesCurrent.length > 0)
      return (
        <React.Fragment>
          {this.state.seriesCurrent.map((series) => (
            <React.Fragment key={series.scheduleSeriesId}>
              <div className="mt-4 col-sm-6 p-0 text-center current-series">
                <div className="">
                  <a
                    className="text-dark"
                    data-toggle="collapse"
                    href={"#series-option-" + series.scheduleSeriesId}
                    role="button"
                    aria-expanded="false"
                    aria-controls="series-option"
                  >
                    <i className="icon-menu-1" />
                    {series.scheduleExercise.exercise.name}
                  </a>
                  <div className="training-subtitle">
                    {series.scheduleExercise.exercise.partId}
                  </div>
                </div>
                <div
                  className="collapse w-75 mx-auto"
                  id={"series-option-" + series.scheduleSeriesId}
                >
                  <div className="">
                    <a
                      className="text-dark"
                      data-toggle="collapse"
                      href={"#series-history-" + series.scheduleSeriesId}
                      role="button"
                      aria-expanded="false"
                      aria-controls="series-history"
                    >
                      <i className="icon-info-circled-alt" /> Ostatni trening
                    </a>
                  </div>
                  <div
                    className="collapse"
                    id={"series-history-" + series.scheduleSeriesId}
                  >
                    {this.renderHistory(series)}
                  </div>
                  <div className="">
                    <a
                      className="text-dark"
                      data-toggle="collapse"
                      href={"#series-swap-" + series.scheduleSeriesId}
                      role="button"
                      aria-expanded="false"
                      aria-controls="series-swap"
                    >
                      <i className="icon-loop" /> zamień ćwiczenia
                    </a>
                  </div>
                  <div
                    className="collapse"
                    id={"series-swap-" + series.scheduleSeriesId}
                  >
                    {this.renderSelectPart(series.scheduleExerciseId)}
                    {this.renderExercises(series.scheduleExerciseId)}
                    <button
                      className="btn btn-outline-primary mt-2"
                      onClick={() => {
                        this.handleChangeExercise(series.scheduleExerciseId);
                      }}
                    >
                      Zamień
                    </button>
                  </div>
                  <div className="">
                    <a
                      className="text-danger"
                      data-toggle="collapse"
                      href={"#series-delete-" + series.scheduleSeriesId}
                      role="button"
                      aria-expanded="false"
                      aria-controls="series-delete"
                    >
                      <i className="icon-trash-empty" /> usuń serię
                    </a>
                  </div>
                  <div
                    className="collapse"
                    id={"series-delete-" + series.scheduleSeriesId}
                  >
                    <p>Potwierdź usunięcie serii</p>
                    <button
                      className="btn btn-outline-danger my-2"
                      onClick={() => {
                        this.handleDeleteSeries(series);
                      }}
                    >
                      Usuń serię
                    </button>
                  </div>
                </div>
                <div className="training-subtitle text-center">
                  {series.order} seria
                </div>
                <div className="pl-2">{series.name}</div>
                {this.renderInput("repeats", series, false)}
                {this.renderInput("load", series, false)}
                {this.renderInput("distance", series, false)}
                {this.renderInput("time", series, false)}
                {this.renderInput("restTime", series, false)}
                <div
                  className="collapse"
                  id={"series-hidden-" + series.scheduleSeriesId}
                >
                  {this.renderInput("repeats", series, true)}
                  {this.renderInput("load", series, true)}
                  {this.renderInput("distance", series, true)}
                  {this.renderInput("time", series, true)}
                  {this.renderInput("restTime", series, true)}
                </div>
                <a
                  className="text-dark"
                  data-toggle="collapse"
                  href={"#series-hidden-" + series.scheduleSeriesId}
                  role="button"
                  aria-expanded="false"
                  aria-controls="series-hidden"
                >
                  rozwiń
                </a>
                <div className="">
                  <button
                    className="btn btn-outline-success mt-2"
                    onClick={() => {
                      this.handleFinishSeries(series);
                    }}
                  >
                    Zakończ serię
                  </button>
                </div>
              </div>
            </React.Fragment>
          ))}
        </React.Fragment>
      );
    else return null;
  }
  renderFinished() {
    if (this.state.seriesCompleted.length > 0)
      return (
        <React.Fragment>
          <div className="col-12 text-center mt-4">
            <a
              className="text-dark font-size-20 font-weight-300"
              data-toggle="collapse"
              href="#series-finished"
              role="button"
              aria-expanded="false"
              aria-controls="series-finished"
            >
              Serie zakończone
            </a>
          </div>
          <div className="collapse col-12" id="series-finished">
            <div className="row">
              {this.state.seriesCompleted.map((series) => (
                <React.Fragment key={series.scheduleSeriesId}>
                  <div className="mt-3 col-sm-4 p-0 text-center current-series">
                    <div className="">
                      {series.scheduleExercise.exercise.name}
                    </div>
                    <div className="training-subtitle text-center">
                      {series.order} seria
                    </div>
                    <div className="pl-2">{series.name}</div>
                  </div>
                </React.Fragment>
              ))}
            </div>
          </div>
        </React.Fragment>
      );
    else return null;
  }
  renderToComplete() {
    if (this.state.seriesToComplete.length > 0)
      return (
        <React.Fragment>
          <div className="col-12 text-center mt-4">
            <a
              className="text-dark font-size-20 font-weight-300"
              data-toggle="collapse"
              href="#series-to-complete"
              role="button"
              aria-expanded="false"
              aria-controls="series-to-complete"
            >
              Pozostałe do zrobienia
            </a>
          </div>
          <div className="collapse col-12" id="series-to-complete">
            <div className="row">
              {this.state.seriesToComplete.map((series) => (
                <React.Fragment key={series.scheduleSeriesId}>
                  <div className="mt-4 col-sm-6 p-0 text-center current-series">
                    <div className="">
                      <a
                        className="text-dark"
                        data-toggle="collapse"
                        href={"#series-option-" + series.scheduleSeriesId}
                        role="button"
                        aria-expanded="false"
                        aria-controls="series-option"
                      >
                        <i className="icon-menu-1" />
                        {series.scheduleExercise.exercise.name}
                      </a>
                      <div className="training-subtitle">
                        {series.scheduleExercise.exercise.partId}
                      </div>
                    </div>
                    <div
                      className="collapse w-75 mx-auto"
                      id={"series-option-" + series.scheduleSeriesId}
                    >
                      <div className="">
                        <a
                          className="text-dark"
                          data-toggle="collapse"
                          href={"#series-history-" + series.scheduleSeriesId}
                          role="button"
                          aria-expanded="false"
                          aria-controls="series-history"
                        >
                          <i className="icon-info-circled-alt" /> Ostatni
                          trening
                        </a>
                      </div>
                      <div
                        className="collapse"
                        id={"series-history-" + series.scheduleSeriesId}
                      >
                        {this.renderHistory(series)}
                      </div>
                      <div className="">
                        <a
                          className="text-dark"
                          data-toggle="collapse"
                          href={"#series-swap-" + series.scheduleSeriesId}
                          role="button"
                          aria-expanded="false"
                          aria-controls="series-swap"
                        >
                          <i className="icon-loop" /> zamień ćwiczenia
                        </a>
                      </div>
                      <div
                        className="collapse"
                        id={"series-swap-" + series.scheduleSeriesId}
                      >
                        {this.renderSelectPart(series.scheduleExerciseId)}
                        {this.renderExercises(series.scheduleExerciseId)}
                        <button
                          className="btn btn-outline-primary mt-2"
                          onClick={() => {
                            this.handleChangeExercise(
                              series.scheduleExerciseId
                            );
                          }}
                        >
                          Zamień
                        </button>
                      </div>
                      <div className="">
                        <a
                          className="text-danger"
                          data-toggle="collapse"
                          href={"#series-delete-" + series.scheduleSeriesId}
                          role="button"
                          aria-expanded="false"
                          aria-controls="series-delete"
                        >
                          <i className="icon-trash-empty" /> usuń serię
                        </a>
                      </div>
                      <div
                        className="collapse"
                        id={"series-delete-" + series.scheduleSeriesId}
                      >
                        <p>Potwierdź usunięcie serii</p>
                        <button
                          className="btn btn-outline-danger my-2"
                          onClick={() => {
                            this.handleDeleteSeries(series);
                          }}
                        >
                          Usuń serię
                        </button>
                      </div>
                    </div>
                    <div className="training-subtitle text-center">
                      {series.order} seria
                    </div>
                    <div className="pl-2">{series.name}</div>
                    {this.renderInput("repeats", series, false)}
                    {this.renderInput("load", series, false)}
                    {this.renderInput("distance", series, false)}
                    {this.renderInput("time", series, false)}
                    {this.renderInput("restTime", series, false)}
                    <div
                      className="collapse"
                      id={"series-hidden-" + series.scheduleSeriesId}
                    >
                      {this.renderInput("repeats", series, true)}
                      {this.renderInput("load", series, true)}
                      {this.renderInput("distance", series, true)}
                      {this.renderInput("time", series, true)}
                      {this.renderInput("restTime", series, true)}
                    </div>
                    <a
                      className="text-dark"
                      data-toggle="collapse"
                      href={"#series-hidden-" + series.scheduleSeriesId}
                      role="button"
                      aria-expanded="false"
                      aria-controls="series-hidden"
                    >
                      rozwiń
                    </a>
                    <div className="">
                      <button
                        className="btn btn-outline-success mt-2"
                        onClick={() => {
                          this.handleFinishSeries(series);
                        }}
                      >
                        Zakończ serię
                      </button>
                    </div>
                  </div>
                </React.Fragment>
              ))}
            </div>
          </div>
        </React.Fragment>
      );
    else return null;
  }
  renderExercises(scheduleExerciseId) {
    if (this.state.exercises.length > 0) {
      return (
        <React.Fragment>
          <select
            id={"select-exercises-" + scheduleExerciseId}
            className="custom-select"
          >
            {this.state.exercises.map((e) => (
              <option key={e.exerciseId} value={e.exerciseId}>
                {e.name}
              </option>
            ))}
          </select>
        </React.Fragment>
      );
    }
  }
  renderFinishTraining() {
    return (
      <React.Fragment>
        <div className="col-8 mx-auto">
          <h2>Zakończ trening</h2>
          <input
            type="checkbox"
            defaultChecked
            id="next-training-checkbox"
          />{" "}
          Stworzyć nowy trening
          <input
            type="datetime-local"
            className="form-control"
            id="next-training-date"
            defaultValue={new Date(this.state.nextTrainingDate)
              .toISOString()
              .substr(0, 16)}
          />
          <input type="checkbox" defaultChecked id="update-series-checkbox" />{" "}
          Aktualizować serię w ćwiczeniach
          <input
            type="datetime-local"
            className="form-control"
            id="finish-training-date"
            defaultValue={new Date().toISOString().substr(0, 16)}
          />
          <button
            className="btn btn-outline-success mt-2"
            onClick={this.handleFinishTraining}
          >
            Zakończ trening
          </button>
        </div>
      </React.Fragment>
    );
  }

  renderBody() {
    if (this.state.loading) {
      return (
        <React.Fragment>
          <h3>Trwa ładowanie</h3>
        </React.Fragment>
      );
    }

    return (
      <React.Fragment>
        <h2 className="text-center col-12 mt-2">{this.state.training.name}</h2>
        {this.renderCurrent()}
        {this.renderToComplete()}
        {this.renderFinishTraining()}
        {this.renderFinished()}
      </React.Fragment>
    );
  }
  render() {
    return this.renderBody();
  }
}
