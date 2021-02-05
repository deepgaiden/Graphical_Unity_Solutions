# Graphical_Unity_Solutions
Here it will be implemented the graphical side of the octoguy simulation implementation.

## Understanding inverse kinematics
Originally the inverse kinematics was develop for robotics, so the terminology is derivative from that context. For each joint we have a degree of freedom, that is, a numeric value necessary for describing the system state. 

<div style = 'text-align:center'>

![fig1](./README_multimedia/fig1.jpg)

**Figure 1**: Robot with 6 degrees of freedom.

</div>

The end effector can be counted or not as a degree of freedom. As we want the arm to reach this point so he will not be, as we will give this point to the engine and the other degrees of freedom will be calculated.
When we give the state of each degree of freedom and from that we acquire the **end effector** state, that is called **Forward kinematics**. It is an easy and explicite problem.
It cam be expressed mathematically as:


<div style='text-align:center'>

$
P_i = P_{i-1} + rotate \left( D_i,P_{i-1},\sum_{k=0}^{i-1}\alpha_k   \right)
$

</div>

So the position of a point is equal to the position of the previous point plus the movement caused from the rotation of the last points. This can be calculated with a Unity function that take the distance from the point, the point and the rotations as arguments. 

In unity we can cause a game object to inherit the parent position, rotation and scale with parenting. The bones, if imported correctly from blender will come already with the right parenting relations.

For the backward kinematics this isn't enough, we will need to simulate this without moving the bones for implementing the **Inverse Kinematics Gradient Descent**. So we will need to implement an mathematical approach so that we can test the states without to much computations stress.
