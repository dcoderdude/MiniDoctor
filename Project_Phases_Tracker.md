### **Game Concept Recap: Doctor Interaction Game**

**Objective:**
Help your daughter overcome fear/trauma of visiting the doctor by interacting with a friendly, responsive game environment.

---

**Screen Layout:**

* **Left Side:** Full-body patient in a doctor’s office.

  * Patient is static by default.
  * Reacts dynamically:

    * Shows discomfort when there is a problem area.
    * Shows positive feedback when a problem area is fixed.
  * Provides guidance:

    * Example verbal cue: “I have an ouch in my ear.”
    * Repeat in Spanish: “Tengo un dolor en la oreja.”
    * Loops until problem is fixed.

* **Right Side:** Body parts container.

  * Holds individual body parts (e.g., ear, hand) **separate from the patient sprite**.
  * Problem areas are highlighted and play sound to draw attention.
  * Player interacts by touching the problem body part to “fix” it.

**Gameplay Mechanics:**

* Problem areas can have objects stuck (e.g., crayon in the ear).
* Player selects/fixes the problem area using touch.
* Positive feedback reinforces successful interaction.

**Scene Hierarchy (current setup):**

* Main (Node2D)

  * Background (ColorRect)
  * Patient (Node2D)

    * \[Future] PatientSprite (Sprite2D)
    * BodyPart (Node2D)

      * ObjectToRemove (Node2D)

        * BodyPartRect (ColorRect)

          * EarSprite (Sprite2D)
          * ObjectRectArea (Area2D)

            * ObjectRect (Sprite2D)
            * CollisionShape2D
  * UI (Control)

    * InstructionLabel (Label)
  * Audio (Node)

    * ProblemSound
    * FixedSound

**Notes / Next Steps:**

* Add **PatientSprite** to visualize the left side.
* Align all body parts and patient sprite for **tablet-friendly proportions**.
* Implement **dynamic reactions and guidance** for patient.
* Ensure **right-side interactions** trigger sound, highlight, and update patient feedback.
