import send from "./RequestSender.js";

export default class TaskService {
    createTask() {
        return send("/Tasks/CreateTask", "post", {});
    }
    removeTask(id) {
        return send("/Tasks/RemoveTask", "post", { id: id });
    }
    updateTask({ id, completed, text, expirationTime }) {
        return send("/Tasks/UpdateTask", "post", { id, completed, text, expirationTime });
    }
}