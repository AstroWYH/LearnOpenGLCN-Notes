#version 330 core
out vec4 FragColor;

struct Material {
    sampler2D diffuse; // wyh 漫反射贴图 = 纹理, 物体材质跟纹理建立起关系, 跟Games101第8课的glsl很像了
    sampler2D specular; // wyh 这东西是个采样器, texture(material.diffuse, TexCoords)才是物体材质贴上纹理的颜色?材质本不起作用, 就是采样器?
    float shininess;
}; 

struct Light {
    vec3 position;

    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
};

in vec3 FragPos;  
in vec3 Normal;  
in vec2 TexCoords;
  
uniform vec3 viewPos;
uniform Material material;
uniform Light light;

void main()
{
    // ambient
    vec3 ambient = light.ambient * texture(material.diffuse, TexCoords).rgb; // wyh 环境光, 跟漫反射的材质颜色相同

    // diffuse 
    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(light.position - FragPos);
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = light.diffuse * diff * texture(material.diffuse, TexCoords).rgb; // wyh 记住并理解texture(material.diffuse, TexCoords)这个表达式
    
    // specular
    vec3 viewDir = normalize(viewPos - FragPos);
    vec3 reflectDir = reflect(-lightDir, norm);  
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
    vec3 specular = light.specular * spec * texture(material.specular, TexCoords).rgb; // wyh 镜面光的贴图来源很有意思, 是漫反射贴图(即纹理)某部分特殊化来的
    // wyh 其实漫反射贴图就是第一张纹理了, 此时的4.1就是纹理加上一个正常的高光项, 高光仍是加在整个物体上的
    // wyh 而4.2则是把高光项换成了第二张纹理(木箱金属边框), 所以此时的高光项只加在第二张纹理上, 所以可以造成只有边框高光的效果 

    vec3 result = ambient + diffuse + specular;
    FragColor = vec4(result, 1.0);
} 