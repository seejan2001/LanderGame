# 🛸 Decoupled Space Shooter (Unity)

A high-performance, modular 2D space arcade shooter engineered with an **Event-Driven Architecture** using standard .NET design patterns to achieve clean code separation.

🎮 **[Click Here to Play the Live WebGL Build on Itch.io](https://sjxexe.itch.io/decoupled-space-shooter)**

---

## 🛠️ Architectural Highlights & Engineering Patterns

Instead of relying on heavy performance-draining dependencies (`GetComponent`, `FindObjectOfType`) or messy Singleton managers, this project separates core gameplay data from visual and audio layers.

* **Standard .NET Event Patterns:** Engineered using native C# `event EventHandler` signatures, adhering to enterprise-grade software standards.
* **Strongly-Typed Payloads:** Leveraged custom data objects extending `EventArgs` to pass clean information packets (like exact damage values or score counts) safely between scripts.
* **Zero System Coupling:** Gameplay mechanics run entirely independently of interface layers. You can completely delete the UI from a scene, and the game loop will still run perfectly without throwing `NullReferenceException` errors.
* **Memory Management:** Implements strict event subscription handling inside Unity's `OnEnable` and `OnDisable` lifecycles to ensure zero memory leaks over long gameplay sessions.

---

## 📁 Key Source Files to Review

Feel free to browse the core architecture scripts within the repository:

* 📄 **Central Event Hub:** Contains your global event declarations using standard C# `EventHandler` signatures.
* 📄 **Player Controller / Publisher:** Handles movement boundaries and physics states while firing standard .NET events without holding direct references to UI or Sound systems.
* 📄 **UI / Audio Subscribers:** Listen to incoming event streams reactively and update HUD text fields dynamically without tight coupling.

---

## 💻 Tech Stack & Tools Used
* **Engine:** Unity (WebGL Build Support Pipeline)
* **Language:** C# (.NET Framework Event Handlers)
* **Platform:** WebGL / HTML5 
* **Deployment:** Hosted live on Itch.io
