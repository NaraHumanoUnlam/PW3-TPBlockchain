// SPDX-License-Identifier: MIT
pragma solidity ^0.8.6;

contract TasksContract {
    uint256 public tasksCounter = 0;

    struct Task {
        uint256 id;
        string title;
        string description;
        bool done;
        uint256 createdAt;
    }

    event TaskCreated(
        uint256 id,
        string title,
        string description,
        bool done,
        uint256 createdAt
    );
    event TaskToggledDone(uint256 id, bool done);

    mapping(uint256 => Task) public tasks;

    constructor() {
        createTask("Primer tarea", "primer tarea por defecto");
    }

    function createTask(string memory _title, string memory _description)
        public
    {
        tasksCounter++;
        tasks[tasksCounter] = Task(
            tasksCounter,
            _title,
            _description,
            false,
            block.timestamp
        );
        emit TaskCreated(
            tasksCounter,
            _title,
            _description,
            false,
            block.timestamp
        );
    }

    function toggleDone(uint256 _id) public {
        Task memory _task = tasks[_id];
        _task.done = !_task.done;
        tasks[_id] = _task;
        emit TaskToggledDone(_id, _task.done);
    }

    function getTask(uint256 _id)
        public
        view
        returns (
            uint256 id,
            string memory title,
            string memory description,
            bool done,
            uint256 createdAt
        )
    {
        Task memory _task = tasks[_id];
        return (
            _task.id,
            _task.title,
            _task.description,
            _task.done,
            _task.createdAt
        );
    }

    function getTasks() public view returns (Task[] memory) {
        Task[] memory _tasks = new Task[](tasksCounter);
        for (uint256 i = 1; i <= tasksCounter; i++) {
            _tasks[i - 1] = tasks[i];
        }
        return _tasks;
    }

    function deleteTask(uint256 _id) public {
        delete tasks[_id];
    }

    function updateTask(
        uint256 _id,
        string memory _title,
        string memory _description
    ) public {
        Task memory _task = tasks[_id];
        _task.title = _title;
        _task.description = _description;
        tasks[_id] = _task;
    }

    //get address
    function getAddress() public view returns (address) {
        return address(this);
    }


}
