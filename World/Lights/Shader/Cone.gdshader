shader_type spatial;
render_mode cull_disabled, unshaded;

group_uniforms Dissolve;
uniform bool invert = false;
uniform float model_height = 3.0;
uniform float dissolve_start: hint_range(0.0, 1.0) = 0.001;
uniform float dissolve_length: hint_range(0.0, 1.0) = 1.0;
uniform float gradient_bias: hint_range(0.1, 5.0) = 1.0;
uniform vec4 color: source_color = vec4(0.5,1.0,1.0, 1.0);

varying float vert_height;
varying vec2 v_uv;

void vertex() {
	if (invert) {
		vert_height = (-VERTEX.y + (model_height / 2.0)) / model_height;
	} else {
		vert_height = (VERTEX.y + (model_height / 2.0)) / model_height;
	}

	v_uv = UV;
}

void fragment(){
	float gradient_height = vert_height - dissolve_start;
	gradient_height *= 1.0 / dissolve_length;
	gradient_height = clamp(pow(gradient_height, gradient_bias), 0.0, 1.0);

	ALPHA = mix(1.0, 0.0, gradient_height) * color.a;
	ALBEDO = color.xyz * vec3(1.0 - gradient_height);
}