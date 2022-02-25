#version 330 core
out vec4 FragColor;

struct Material {
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;    
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
  
uniform vec3 viewPos; // wyh 观察位置, 即摄像机位置, 世界空间中
uniform Material material; // wyh Material像内置结构体, 物体材质的颜色
/* struct Material {
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
    float shininess; // wyh 所以材质就是这个结构体?
};  */
uniform Light light; // wyh Light像内置结构提, 灯的颜色, 我可能瞎了, 他们不是内置结构体
/* struct Light {
    vec3 position;
    vec3 ambient;
    vec3 diffuse;
    vec3 specular; // wyh 所以这就是灯(光源)的结构体?
}; */


void main()
{
    // ambient
    vec3 ambient = light.ambient * material.ambient;
  	
    // diffuse 
    vec3 norm = normalize(Normal); // wyh vs传来的法线, 1个顶点1个法线
    vec3 lightDir = normalize(light.position - FragPos); // wyh 1个顶点1个光照方向单位向量
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = light.diffuse * (diff * material.diffuse); // wyh 不懂就看2.2教程
    
    // specular
    vec3 viewDir = normalize(viewPos - FragPos); // wyh 1个顶点1个观察方向单位向量
    vec3 reflectDir = reflect(-lightDir, norm); // wyh 光的镜面反射单位向量
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess); // wyh material.shininess为镜面高光的散射/半径, 亦为余弦强度绘制
    vec3 specular = light.specular * (spec * material.specular);

    vec3 result = ambient + diffuse + specular; // wyh 实际上你看到的颜色, 主要就是灯的颜色 * 物体的颜色 * 各种不同的程度, 当然顶点/视角位置不同也不一样
    FragColor = vec4(result, 1.0); // wyh 这就是所谓的: 逐顶点着色
} 