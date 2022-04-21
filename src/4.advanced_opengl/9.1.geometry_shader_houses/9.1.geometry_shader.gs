#version 330 core
layout (points) in; // wyh gs的in
layout (triangle_strip, max_vertices = 5) out; // wyh gs的out
// wyh 教程: max_vertices几何着色器同时希望我们设置一个它最大能够输出的顶点数量（如果你超过了这个值，OpenGL将不会绘制多出的顶点

in VS_OUT { // wyh 8.1讲的接口块, 块名VS_OUT名字是不变的(和vs一样), gs_in实例名是可以变化的
    vec3 color;
} gs_in[]; // wyh 注意接口块的[], 类似数组

out vec3 fColor;

void build_house(vec4 position)
{    
    fColor = gs_in[0].color; // gs_in[0] since there's only one input vertex
    gl_Position = position + vec4(-0.2, -0.2, 0.0, 0.0); // 1:bottom-left   
    EmitVertex();   
    gl_Position = position + vec4( 0.2, -0.2, 0.0, 0.0); // 2:bottom-right
    EmitVertex();
    gl_Position = position + vec4(-0.2,  0.2, 0.0, 0.0); // 3:top-left
    EmitVertex();
    gl_Position = position + vec4( 0.2,  0.2, 0.0, 0.0); // 4:top-right
    EmitVertex();
    gl_Position = position + vec4( 0.0,  0.4, 0.0, 0.0); // 5:top
    fColor = vec3(1.0, 1.0, 1.0);
    EmitVertex(); // wyh 发射顶点, 抽象
    EndPrimitive(); // wyh 教程: 当EndPrimitive被调用时，所有发射出的(Emitted)顶点都会合成为指定的输出渲染图元
    // wyh 一般是多个EmitVertex和1个EndPrimitive
    // wyh 9.1核心: 1个点通过位置变化生成5个点, 然后渲染成gs的out三角形, 最终得到4个房子
}

void main() {    
    build_house(gl_in[0].gl_Position);
}

// wyh 新增gs几何着色器