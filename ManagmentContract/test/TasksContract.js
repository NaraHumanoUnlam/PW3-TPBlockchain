const TasksContract = artifacts.require("TasksContract");

contract("TasksContract", (accounts) => {
  before(async () => {
    this.tasksContract = await TasksContract.deployed();
  });

  it("migrate deployed successfully", async () => {
    const address = await this.tasksContract.address;

    assert.notEqual(address, null);
    assert.notEqual(address, undefined);
    assert.notEqual(address, 0x0);
    assert.notEqual(address, "");
  });

  it("get Tasks List", async () => {
    const tasksCounter = await this.tasksContract.tasksCounter();
    const task = await this.tasksContract.tasks(tasksCounter);

    assert.equal(task.id.toNumber(), tasksCounter.toNumber());
    assert.equal(task.title, "Primer tarea");
    assert.equal(task.description, "primer tarea por defecto");
    assert.equal(task.done, false);
    assert.equal(tasksCounter, 1);
  });

  it("task created successfully", async () => {
    const result = await this.tasksContract.createTask("segunda tarea", "creacion de test con una segunda tarea");
    const taskEvent = result.logs[0].args;
    const tasksCounter = await this.tasksContract.tasksCounter();

    assert.equal(tasksCounter, 2);
    assert.equal(taskEvent.id.toNumber(), 2);
    assert.equal(taskEvent.title, "segunda tarea");
    assert.equal(taskEvent.description, "creacion de test con una segunda tarea");
    assert.equal(taskEvent.done, false);
  });

  it("task toggled done", async () => {
    const result = await this.tasksContract.toggleDone(1);
    const taskEvent = result.logs[0].args;
    const task = await this.tasksContract.tasks(1);

    assert.equal(task.done, true);
    assert.equal(taskEvent.id.toNumber(), 1);
    assert.equal(taskEvent.done, true);
  });

  

  it("task not found", async () => {
    try {
      await this.tasksContract.tasks(3);
    } catch (error) {
      assert(error.message.indexOf("revert") >= 0);
    }
  });
});
